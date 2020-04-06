using System;
using System.Collections.Generic;

namespace CodeProjectSerialComms
{
    public class Trilateration
    {
        private bool isWrongWay = false;

        public Vector GetIntersectionPoint(string P1s, string P2s, string P3s, string radiuses)
        {
            Vector p1 = new Vector(P1s);
            Vector p2 = new Vector(P2s);
            Vector p3 = new Vector(P3s);
            Vector radii = new Vector(radiuses);
            Vector res = new Vector();
            var positions = Trilaterate(p1, p2, p3, radii);

            if (positions != null && positions[0].valid)
            {
                Vector point;

                if (positions[0].y < positions[1].y)
                {
                    point = positions[1];
                }
                else
                    point = positions[0];

                point.final = true;
                point.valid = true;
                res = point;
            }

            //if (!positions[0].valid)
            //    return positions[0];
            //else
            //{
            //    Vector ip = new Vector();

            //    if (positions[0].final)
            //    {
            //        if (positions.Length > 1)
            //        {
            //            if (positions[0].y < positions[1].y)
            //            {
            //                res = positions[1];
            //            }
            //            else
            //            {
            //                res = positions[0];
            //            }
            //        }
            //        else
            //        {
            //            res = positions[0];
            //        }
            //    }
            //    else
            //    {
            //        ip = positions[0];
            //        float r1 = (float)Math.Sqrt((Math.Pow(ip.x - p1.x, 2) + Math.Pow(ip.y - p1.y, 2) + Math.Pow(ip.z - p1.z, 2)));
            //        float r2 = (float)Math.Sqrt((Math.Pow(ip.x - p2.x, 2) + Math.Pow(ip.y - p2.y, 2) + Math.Pow(ip.z - p2.z, 2)));
            //        float r3 = (float)Math.Sqrt((Math.Pow(ip.x - p3.x, 2) + Math.Pow(ip.y - p3.y, 2) + Math.Pow(ip.z - p3.z, 2)));

            //        var finalRes = Trilaterate(p1, p2, p3, new Vector(r1, r2, r3));

            //        if (finalRes.Length > 1)
            //        {
            //            if (finalRes[0].y < finalRes[1].y)
            //            {
            //                res = finalRes[1];
            //            }
            //            else
            //            {
            //                res = finalRes[0];
            //            }
            //        }
            //        else
            //        {
            //            res = finalRes[0];
            //        }
            //    }
            //}

            return res;
        }

        public Vector GetDistancesGivenPosition(Vector position, Vector beacon1Pos, Vector beacon2Pos, Vector beacon3Pos)
        {
            Vector res = new Vector();

            res.x = (float)(Math.Sqrt(Math.Pow(position.x - beacon1Pos.x, 2) + Math.Pow(position.y - beacon1Pos.y, 2) + Math.Pow(position.z - beacon1Pos.z, 2)));
            res.y = (float)(Math.Sqrt(Math.Pow(position.x - beacon2Pos.x, 2) + Math.Pow(position.y - beacon2Pos.y, 2) + Math.Pow(position.z - beacon2Pos.z, 2)));
            res.z = (float)(Math.Sqrt(Math.Pow(position.x - beacon3Pos.x, 2) + Math.Pow(position.y - beacon3Pos.y, 2) + Math.Pow(position.z - beacon3Pos.z, 2)));

            return res;
        }

        public Vector[] Trilaterate(Vector P1, Vector P2, Vector P3, Vector radii)
        {
            Vector[] res = null;
            float r1 = radii.x;
            float r2 = radii.y;
            float r3 = radii.z;

            if (!isWrongWay)
            {
                Vector p2MinP1 = P2 - P1;
                Vector e_x = p2MinP1 / p2MinP1.norm();

                Vector p3MinP1 = P3 - P1;
                float i = Vector.dot(e_x, p3MinP1);
                Vector a = e_x * i;
                Vector tmp = p3MinP1 - a;

                Vector e_y = tmp / tmp.norm();
                Vector e_z = e_x * e_y;

                float d = p2MinP1.norm();
                float j = Vector.dot(e_y, p3MinP1);
                float x = (r1 * r1 - r2 * r2 + d * d) / (2 * d);
                float y = (r1 * r1 - r3 * r3 - 2 * i * x + i * i + j * j) / (2 * j);
                float assert = r1 * r1 - x * x - y * y;

                if (assert < 0 || float.IsNaN(assert))
                {
                    //res = trilaterateThreeCircles(P1, P2, P3, r1, r2, r3);
                    res = new Vector[]
                    {
                        new Vector()
                        {
                            valid = false,
                            final = true
                        }
                    };
                }
                else
                {
                    res = calculateThreeSphereIntersection(e_x, e_y, e_z, P1, x, y, (float)Math.Sqrt(assert));
                }
            }
            else
            {
                res = trilaterateThreeCircles(P1, P2, P3, r1, r2, r3);
            }

            return res;
        }

        private Vector[] trilaterateThreeCircles(Vector P1, Vector P2, Vector P3, float r1, float r2, float r3)
        {
            float theta;
            float[,] rotation = null;
            float[,] reverseRotation = null;
            Vector offset = new Vector();
            Vector[] res = null;

            // sort points by distance for circle-circle intersection if needed 
            if ((P2 - P1).norm() > (P3 - P1).norm())
            {
                Vector temp = P3;
                P3 = P2;
                P2 = temp;
                float tempR = r3;
                r3 = r2;
                r2 = tempR;
            }

            if (P1 != Vector.zero)
            {
                offset = -P1;
                P1 = P1 + offset;
                P2 = P2 + offset;
                P3 = P3 + offset;
            }

            P2.z = 0;
            P3.z = 0;

            if (P2.y != 0)
            {
                float rad = (float)Math.Acos(P2.x / P2.norm());
                theta = (float)(rad / Math.PI) * 180f;

                if (P2.y > 0)
                {
                    theta = -theta;
                    rad = -rad;
                }

                rotation = new float[,]
                {
                        { (float)Math.Cos(rad), -(float)Math.Sin(rad), 0},
                        { (float)Math.Sin(rad),  (float)Math.Cos(rad), 0},
                        { 0, 0, 0 }
                };

                reverseRotation = new float[,]
                {
                        { (float)Math.Cos(-rad), -(float)Math.Sin(-rad), 0},
                        { (float)Math.Sin(-rad),  (float)Math.Cos(-rad), 0},
                        { 0, 0, 0 }
                };

                P2 = rotation.MultiplyMatrix(P2);
                P3 = rotation.MultiplyMatrix(P3);
            }

            res = testCircleCircleIntersection(P1, P2, P3, r1, r2, r3);

            if (res == null)
            {
                res = calculateCircleCircleIntersection(P1, P2, P3, r1, r2, r3);

                Vector avg = Vector.zero;
                //int count = 0;
                //List<Vector> archa = new List<Vector>();

                for (int ind = 0; ind < res.Length; ind++)
                {
                    if (reverseRotation != null)
                        res[ind] = reverseRotation.MultiplyMatrix(res[ind]);

                    res[ind] = res[ind] - offset;

                    //if (res[ind].y >= 0)
                    //{
                    //    avg += res[ind];
                    //    count++;
                    //    archa.Add(res[ind]);
                    //}
                }

                //avg /= count;
                //res = null;
                //avg.valid = true;
                //res = new Vector[] { avg };
            }

            return res;
        }

        private Vector[] calculateThreeSphereIntersection(Vector e_x, Vector e_y, Vector e_z, Vector P1, float x, float y, float z)
        {
            Vector[] res = null;
            Vector exx = e_x * x;
            Vector eyy = e_y * y;
            Vector ezz = e_z * z;

            if (ezz.IsNanOrInfinity())
            {
                ezz = Vector.zero;
            }

            Vector p1Addexx = P1 + exx;
            Vector ndPlus = eyy + ezz;
            Vector ndMinus = eyy - ezz;

            Vector p12A = p1Addexx + ndPlus;
            p12A.valid = true;
            p12A.final = true;

            Vector p12B = p1Addexx + ndMinus;
            p12B.valid = true;
            p12B.final = true;

            res = new Vector[] { p12A, p12B };

            return res;
        }

        private Vector[] testCircleCircleIntersection(Vector P1, Vector P2, Vector P3, float r1, float r2, float r3)
        {
            List<Vector> res = new List<Vector>();
            Vector originalP1 = P1, originalP2 = P2, originalP3 = P3;
            float originalR1 = r1, originalR2 = r2, originalR3 = r3;
            int combinations = 3, combination = 0;

            while (true)
            {
                float d = (P2 - P1).norm();

                if ((d - r1) > r2)
                {
                    res.Add(
                        new Vector()
                        {
                            valid = false,
                            reason = string.Format("no solutions, circles {0} {1} are separate", P1, P2)
                        });

                }
                else if ((r2 > (d + r1)))
                {
                    res.Add(
                        new Vector()
                        {
                            valid = false,
                            reason = string.Format("no solutions, circles {0} {1} are contained", P1, P2)
                        });
                }
                else if (d == 0 && r1 == r2)
                {
                    res.Add(
                        new Vector()
                        {
                            valid = false,
                            reason = string.Format("no solutions, circles {0} {1} coincide", P1, P2)
                        });
                }

                if (++combination > combinations)
                {
                    if (res.Count > 0)
                    {
                        res.Add(
                              new Vector()
                              {
                                  valid = false,
                                  reason = "no solutions, all circles are separate/contained/coincident"
                              });
                    }
                    else
                    {
                        res = null;
                    }

                    break;
                }
                else if (combination == combinations)
                {
                    Vector oldP1 = P1;
                    P1 = P2;
                    P2 = P3;
                    P3 = oldP1;

                    float oldR1 = r1;
                    r1 = r2;
                    r2 = r3;
                    r3 = oldR1;

                    continue;
                }

                Vector tmp = P3;
                P3 = P2;
                P2 = tmp;
                float temp = r3;
                r3 = r2;
                r2 = temp;
            }

            if (res == null)
                return null;

            return res.ToArray();
        }

        private Vector[] calculateCircleCircleIntersection(Vector P1, Vector P2, Vector P3, float r1, float r2, float r3)
        {
            List<Vector> res = new List<Vector>();

            float d1 = (P2 - P1).norm();
            float x1, y1;
            float? x11, y11;
            x11 = y11 = null;

            if (checkIfInnerTangency(d1, r1, r2))
            {
                x1 = ((r2 * (P1.x - P2.x)) / (r1 - r2)) + P2.x;
                y1 = ((r2 * (P1.y - P2.y)) / (r2 - r1)) + P2.y;
            }
            else if (checkIfOuterTangency(d1, r1, r2))
            {
                x1 = ((r2 * (P1.x - P2.x)) / (r1 + r2)) + P2.x;
                y1 = ((r2 * (P1.y - P2.y)) / (r2 + r1)) + P2.y;
            }
            else
            {
                var vec = getCircleForwardIntersectionPoint(P1, P2, r1, r2);
                x1 = vec[0].x;
                y1 = vec[0].y;
                x11 = vec[1].x;
                y11 = vec[1].y;
            }

            float x2, y2;
            float? x21, y21;
            x21 = y21 = null;
            float d2 = (P3 - P1).norm();

            if (checkIfInnerTangency(d2, r1, r3))
            {
                x2 = ((r3 * (P1.x - P3.x)) / (r1 - r3)) + P3.x;
                y2 = ((r3 * (P1.y - P3.y)) / (r3 - r1)) + P3.y;
            }
            else if (checkIfOuterTangency(d2, r1, r3))
            {
                x2 = ((r3 * (P1.x - P3.x)) / (r1 + r3)) + P3.x;
                y2 = ((r3 * (P1.y - P3.y)) / (r3 + r1)) + P3.y;
            }
            else
            {
                var vec = getCircleForwardIntersectionPoint(P1, P3, r1, r3);
                x2 = vec[0].x;
                y2 = vec[0].y;
                x21 = vec[1].x;
                y21 = vec[1].y;
            }

            float x3, y3;
            float? x31, y31;
            x31 = y31 = null;
            float d3 = (P3 - P2).norm();

            if (checkIfInnerTangency(d3, r2, r3))
            {
                x3 = ((r3 * (P2.x - P3.x)) / (r2 - r3)) + P3.x;
                y3 = ((r3 * (P2.y - P3.y)) / (r3 - r2)) + P3.y;
            }
            else if (checkIfOuterTangency(d3, r2, r3))
            {
                x3 = ((r3 * (P2.x - P3.x)) / (r2 + r3)) + P3.x;
                y3 = ((r3 * (P2.y - P3.y)) / (r3 + r2)) + P3.y;
            }
            else
            {
                var vec = getCircleForwardIntersectionPoint(P2, P3, r2, r3);
                x3 = vec[0].x;
                y3 = vec[0].y;
                x31 = vec[1].x;
                y31 = vec[1].y;
            }

            res.Add(new Vector(x1, y1));

            if (x11.HasValue)
                res.Add(new Vector(x11.Value, y11.Value));

            res.Add(new Vector(x2, y2));

            if (x21.HasValue)
                res.Add(new Vector(x21.Value, y21.Value));

            res.Add(new Vector(x3, y3));

            if (x31.HasValue)
                res.Add(new Vector(x31.Value, y31.Value));

            return res.ToArray();
        }

        private bool checkIfInnerTangency(float d, float r1, float r2)
        {
            return d == Math.Abs(r1 - r2);
        }

        private bool checkIfOuterTangency(float d, float r1, float r2)
        {
            return d == r1 + r2;
        }

        private Vector[] getCircleForwardIntersectionPoint(Vector c1, Vector c2, float r1, float r2)
        {
            Vector[] res = null;
            float val1, val2, test;

            float d = (c1 - c2).norm();
            float a1 = d + r1 + r2;
            float a2 = d + r1 - r2;
            float a3 = d - r1 + r2;
            float a4 = -d + r1 + r2;

            float area = (float)Math.Sqrt(a1 * a2 * a3 * a4) / 4;

            // calculating x values
            val1 = (c1.x + c2.x) / 2 + (c2.x - c1.x) * (r1 * r1 - r2 * r2) / (2 * d * d);
            val2 = 2 * (c1.y - c2.y) * area / (d * d);

            float x1 = val1 + val2;
            float x2 = val1 - val2;

            // calculating y values
            val1 = (c1.y + c2.y) / 2 + (c2.y - c1.y) * (r1 * r1 - r2 * r2) / (2 * d * d);
            val2 = 2 * (c1.x - c2.x) * area / (d * d);

            float y1 = val1 - val2;
            float y2 = val1 + val2;

            test = Math.Abs((x1 - c1.x) * (x1 - c1.x) + (y1 - c1.y) * (y1 - c1.y) - r1 * r1);

            if (test > 0.1)
            {
                float tmp = y1;
                y1 = y2;
                y2 = tmp;
            }
            test = Math.Abs((x1 - c1.x) * (x1 - c1.x) + (y1 - c1.y) * (y1 - c1.y) - r1 * r1);

            res = new Vector[]
            {
                new Vector(x1, y1),
                new Vector(x2, y2)
            };

            return res;
        }
    }
}
