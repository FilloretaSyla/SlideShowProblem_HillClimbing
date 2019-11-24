using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlideShowProblem_HillClimbing
{
    class GenerateSolution
    {
            public static string filePath = @"C:\f\write.txt";
            public static string filePaths = @"C:\f\writefile.txt";
            public static string[] lines = File.ReadAllLines(filePath);
            public static string[] liness = File.ReadAllLines(filePaths);
            public static string[] photoToAdd = new string[lines.Count()];
            public static string[] verticalPhoto = new string[lines.Count()];
            public static string tagsOfSlides = "";
            public static int k = 0;
            public static int nrV = 0;


            public static string func(string[] str)
            {


                for (int i = 0; i < str.Count(); i++)
                {
                    // Console.WriteLine(str[i]);
                }
                for (int i = 0; i < lines.Count(); i++)
                {
                    string[] str1 = new string[str.Length];
                    string[] strTags1 = new string[1] { " " };

                    if (i != 0)

                    {
                        string strT = str[i].Substring(4, str[i].Length - 4);
                        string[] strTags = strT.Split(' ');
                        //if the photo is horizontal just add to slideshow
                        if (str[i].Substring(0, 1) == "H")
                        {

                            AddPhoto(str[i]);
                            photoToAdd[i] = str[i].Substring(str[i].Length - 1, 1);
                            tagsOfSlides += str[i].Substring(4, str[i].Length - 4 - 1) + ",";

                        }
                        /*if the photo is vertical go next and find another photo with whom it has almost same tags,
                         and add them to a slide in a slideshow.
                         */
                        else if (str[i].Substring(0, 1) == "V" && exist(str[i], verticalPhoto) == false)
                        {
                            string st1 = "";
                            string st2 = "";
                            int position = 0;
                            //number of vertical photo
                            nrV++;
                            int m = 0;
                            int n = 0;
                            //go and find another vertical photo which has similaries with this photo based on tags
                            for (int j = 1; j < str.Length; j++)
                            {
                                //Check if the photo is vertical
                                if (str[j].Substring(0, 1) == "V" && exist(str[i], verticalPhoto) == false)
                                {
                                    if (i != j)
                                    {
                                        m = CheckPhoto(str[i].ToString(), str[j].ToString());
                                        if (m > n)
                                        {
                                            int temp = n;
                                            n = m;
                                            m = temp;
                                            position = j;
                                        }
                                    }
                                }
                            }

                            //i run foto veritkale qe jon shtu ne menyre qe mos mu perserit e njejta
                            verticalPhoto[i] = str[i];
                            verticalPhoto[i + 1] = str[position];
                            //CheckPhotoTags(str[i], str[position]);
                            st1 = str[i].Substring(4, str[i].Length - 4 - 1);
                            st2 = str[position].Substring(4, str[i].Length - 4 - 1);

                            tagsOfSlides += CheckPhotoTags(str[i], str[position]) + ",";
                            CheckPhotoTags(str[i], str[position]);
                            photoToAdd[i] = str[i].Substring(str[i].Length - 1, 1) + " " + str[position].Substring(str[position].Length - 1, 1);


                        }
                    }
                }



                // Console.WriteLine(tagsOfSlides);
                return CheckSlidesForFitness(tagsOfSlides.Split(',')).ToString();
                //Console.WriteLine(CheckSlidesForFitness(tagsOfSlides.Split(',')));


                //function per me kontorllu foto veritkale mu pershtat ato me ma shume tags te perbashket
                int CheckPhoto(string S1, string S2)
                {
                    int x = 0;
                    string[] s1 = new string[S1.Length];
                    string[] s2 = new string[S2.Length];
                    s1 = S1.Split(' ');
                    s2 = S1.Split(' ');

                    if (s1.Length >= s2.Length)
                        x = s1.Length;
                    else
                        x = s2.Length;
                    int count = 0;
                    // i=2 qe mos mu marr H ose V mirepo mu merr tagat (H cat) H->0, hapsira->1, cat->2, pra ja nisim prej 2-shit
                    for (int i = 2; i < x; i++)
                    {
                        if (s1[i] == s2[i])
                            count++;
                    }
                    //kthen nr e tageve te njejta
                    return count;
                }

                //Kontrollon fites-interers factor
                string CheckSlidesForFitness(string[] addP)
                {
                    int o = 0, d = 0, n = 0;
                    int y = 0;

                    int count = 0, final = 0;

                    for (int i = 0; i < addP.Length; i++)
                    {
                        addP[i] = addP[i].Trim(' ');
                        if (addP[i] != "" && addP[i + 1] != "")
                        {
                            string S1 = addP[i];
                            string S2 = addP[i + 1];

                            string[] s1 = S1.Split(' ');
                            string[] s2 = S2.Split(' ');

                            int x = 0;

                            if (s1.Length > s2.Length)
                                x = s1.Length;
                            else
                                x = s2.Length;

                            for (int ii = 0; ii < s2.Length; ii++)
                            {
                                o = 0;
                                d = 0;
                                n = 0;
                                count = 0;
                                for (int j = 0; j < s1.Length; j++)
                                {
                                    s1[j] = s1[j].Trim(' ');
                                    s2[ii] = s2[ii].Trim(' ');
                                    if ((s1[j] != "") && (s2[ii] != ""))
                                    {
                                        o = s1[j].Where(z => !s2[ii].Contains(z)).Count();
                                        n = s1[j].Where(z => s2[ii].Contains(z)).Count();
                                        d = s2[ii].Where(z => !s1[j].Contains(z)).Count();
                                    }

                                    count = Math.Min(o, Math.Min(d, n));
                                }

                                final += count;
                            }

                        }

                    }

                    //return 
                    return (final).ToString();
                }
                //kthen tags per nje slide vertikal
                string CheckPhotoTags(string P1, string P2)
                {
                    int x = 0;
                    P1 = P1.Substring(4, P1.Length - 5);
                    P2 = P2.Substring(4, P2.Length - 5);
                    string t = "";
                    string[] s1 = P1.Split(' ');
                    string[] s2 = P2.Split(' ');
                    for (int i = 0; i < s1.Length; i++)
                    {
                        t += s1[i] + " ";
                        //Console.Write(s1[i]);
                    }

                    if (s1.Length >= s2.Length)
                        x = s1.Length;
                    else
                        x = s2.Length;
                    string[] tags = s1;
                    string g = "";
                    int o;
                    string[] pt = new string[x];
                    pt = s1;
                    int y = 0;

                    for (int i = 0; i < s2.Length; i++)
                    {
                        o = 0;
                        for (int j = 0; j < s1.Length; j++)
                        {
                            if (s2[i] != s1[i])
                            {
                                o++;
                            }
                            y++;
                        }
                        if (o == y)
                        {
                            g += s2[i] + " ";
                        }

                    }
                    //Console.WriteLine(t + g);
                    return t + g;

                }
                void AddPhoto(string nrS)
                {
                    string path = @"C:\f\writefile.txt";
                     using (var sw = new StreamWriter(path))
                     {
                         sw.WriteLine(nrS);
                     }
                     //k++;
                     //System.IO.File.WriteAllText(@"C:\f\writefile.txt", nrS);
                     return;
                }
                /*this function checks if the slide is added before on slideshow, this func. is valid just for vertical photo.*/
                bool exist(string s, string[] line1)
                {
                    bool b = false;
                    //string[] stri = new string[filepath.Length];
                    foreach (string line in line1)
                    {
                        if (s == line)
                            b = true;
                    }
                    return b;
                }

            }
        }
    }

