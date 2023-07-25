using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Numerics;
using System.Text.RegularExpressions;




namespace Euler_to_Miller
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }



        public double[] primecvt(double[] mart)//可以将输入的小数数组做互质处理后输出
        {
            double max1;//用于储存最大的分母
            double min1;//用于储存最大的分母
            int a = mart.GetLength(0);//得到输入数组行数
            string[] b = new string[a];//用于储存转化后的分数
            double[] sArray = new double[a];//新建二维数组用于储存转化后的分母
            string[] sArra;

            do
            {
                int count = 0;
                while (count < a)
                {
                    b[count] = this.integercvt(mart[count]);//把mart2中各数转成分数存b
                    sArra = b[count].Split(new char[1] { '/' });//把b中各数按/分割后存到sArra
                    sArray[count] = Convert.ToDouble(sArra[1]);//sArra取分母转double存sArray
                    count++;
                }
                max1 = sArray.Max();//储存最大的分母
                for (int i = 0; i < a; i++)
                {
                    mart[i] = mart[i] * max1;//mart里面各个数乘上最大分母
                }
            }
            while (max1 != 1);//当最大分母为1时退出

            //#region把Mart里面的数取非0的最小数
            
            min1 = 1;//初始值
            for (int i = 0; i < a; i++)//取出非零的一个绝对值做min1的初始值
            {
                if (mart[i] != 0)
                {
                    if (mart[i] < 0)
                    {
                        min1 = -mart[i];
                    }
                    else
                    {
                        min1 = mart[i];
                    }
                    break;
                }
            }

            for (int i = 0; i < a; i++)//取非零最小绝对值min1
            {
                
                if (mart[i] < 0) 
                {
                    if (-mart[i] < min1) { min1 = -mart[i]; }
                }
                else if (mart[i] == 0)
                {

                }
                else 
                {
                    if (mart[i] < min1) { min1 = mart[i]; }
                }

            }

            //#endregion
        
            int a2 = a;
            int n = 0;

            while (n != a2)//当整数数与元素数不同时执行??
            {
                n = 0;
                for (int i = 0; i < a; i++)
                {
                    if (mart[i] != 0)
                    {
                        if ((Convert.ToDouble((int)(mart[i] / min1))) == mart[i] / min1)//判断是不是整数
                        {
                            n++;
                        }
                    }
                    else //如果遇到0就把元素数减1
                    {
                        a2--;
                    }

                }
                min1--;
            }
            min1++;

            for (int i = 0; i < a; i++)
            {
                mart[i] = mart[i] / min1;
            }
            
            return mart;
        }
        public string integercvt(double numb)//可以将输入的小数转换为分数
        {

            string result = default(string);
            try
            {
                if (numb > 6E-17)//6e-17看做0
                {
                    double inputnum = numb;
                    string[] array = inputnum.ToString().Split('.');
                    int len = array[1].Length;
                    long num = Convert.ToInt64(Math.Pow(10, len));
                    long value = Convert.ToInt64(inputnum * num);
                    long a = value;
                    long b = num;
                    while (a != b)
                    {
                        if (a > b)
                            a = a - b;
                        else
                            b = b - a;
                    }
                    value = value / a;
                    num = num / a;
                    result = string.Format("{0}/{1}", value, num);
                }
                else if (numb < -6E-17)
                {
                    double inputnum = numb;
                    string[] array = inputnum.ToString().Split('-', '.');
                    int len = array[2].Length;
                    long num = Convert.ToInt64(Math.Pow(10, len));
                    long value = Convert.ToInt64(-inputnum * num);
                    long a = value;
                    long b = num;
                    while (a != b)
                    {
                        if (a > b)
                            a = a - b;
                        else
                            b = b - a;
                    }
                    value = value / a;
                    num = num / a;
                    result = string.Format("-{0}/{1}", value, num);
                }
                else if (numb < 6E-17 && numb > -6E-17)//6e-17看做0
                {
                    result =  "0/1";
                }
            }
            catch (Exception)
            {
                result = numb.ToString() + "/1";
            }
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var fai = double.Parse(textBox1.Text);//注意var定义在方法内
                var fai1 = double.Parse(textBox2.Text);
                var fai2 = double.Parse(textBox3.Text);

                //创建A，B，C，D矩阵
                double[] matrixA = new double[3] { Math.Cos(Math.PI * fai1 / 180) * Math.Cos(Math.PI * fai2 / 180) - Math.Sin(Math.PI * fai1 / 180) * Math.Sin(Math.PI * fai2 / 180) * Math.Cos(Math.PI * fai / 180), -Math.Sin(Math.PI * fai2 / 180) * Math.Cos(Math.PI * fai1 / 180) - Math.Sin(Math.PI * fai1 / 180) * Math.Cos(Math.PI * fai2 / 180) * Math.Cos(Math.PI * fai / 180) ,  Math.Sin(Math.PI * fai1 / 180) * Math.Sin(Math.PI * fai / 180)};
                double[] matrixB = new double[3]{ Math.Sin(Math.PI * fai / 180) * Math.Sin(Math.PI * fai2 / 180) ,  Math.Sin(Math.PI * fai / 180) * Math.Cos(Math.PI * fai2 / 180) ,  Math.Cos(Math.PI * fai / 180) } ;

                //2个矩阵相乘，要注意矩阵乘法的维数要求
                var m = matrixA;
                var n= matrixB;
                m = primecvt(m);
                n = primecvt(n);
                textBox7.Text = "(" + m[0].ToString() + "," + m[1].ToString() + "," + m[2].ToString() + ")" + "[" + n[0].ToString() + "," + n[1].ToString() + "," + n[2].ToString() + "]";
            }
            catch (Exception)
            {
                MessageBox.Show("Unexpected error");
            }


        }
        private void button2_Click(object sender, EventArgs e)//fcc m to e
        {
            try
            {
                double L=System.Convert.ToDouble(textBoxL.Text);
                double K = System.Convert.ToDouble(textBoxK.Text);
                double H = System.Convert.ToDouble(textBoxH.Text);
                double U = System.Convert.ToDouble(textBoxU.Text);
                double V = System.Convert.ToDouble(textBoxV.Text);
                double W = System.Convert.ToDouble(textBoxW.Text);
                double fai=180*Math.Acos(L/(Math.Sqrt((Math.Pow(H,2)+Math.Pow(K,2)+Math.Pow(L,2)))))/Math.PI;
                double fai1 = 180 * (Math.Asin((W / (Math.Sqrt((Math.Pow(U, 2) + Math.Pow(V, 2) + Math.Pow(W, 2))))) * ((Math.Sqrt((Math.Pow(H, 2) + Math.Pow(K, 2) + Math.Pow(L, 2))) / (Math.Pow(H, 2) + Math.Pow(K, 2)))))) / Math.PI;
                double fai2 = 180 * Math.Acos(K / (Math.Sqrt((Math.Pow(H, 2) + Math.Pow(K, 2))))) / Math.PI;
                textBox7.Text = "φ1=" + fai1.ToString() + " φ=" + fai.ToString() + " φ2=" + fai2.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Unexpected error");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var fai = double.Parse(textBox1.Text);//注意var定义在方法内
                var fai1 = double.Parse(textBox2.Text);
                var fai2 = double.Parse(textBox3.Text);
                var ca = double.Parse(textBox4.Text);

                //创建A，B，C，D矩阵
                var matrixA = DenseMatrix.OfArray(new[,] { { System.Math.Sqrt(3) / 2, -1 / 2, 0 }, { 0, 1, 0 }, { System.Math.Sqrt(3) / -2, -1 / 2, 0 }, { 0, 0, ca } });
                var matrixB = DenseMatrix.OfArray(new[,] { { 2 / 3, -1 / 3, 0 }, { 0, 2 / 3, 0 }, { -2 / 3, -1 / 3, 0 }, { 0, 0, ca } });
                var matrixC = DenseMatrix.OfArray(new[,] { { Math.Cos(Math.PI * fai1 / 180) * Math.Cos(Math.PI * fai2 / 180) - Math.Sin(Math.PI * fai1 / 180) * Math.Sin(Math.PI * fai2 / 180) * Math.Cos(Math.PI * fai / 180) }, { -Math.Sin(Math.PI * fai2 / 180) * Math.Cos(Math.PI * fai1 / 180) - Math.Sin(Math.PI * fai1 / 180) * Math.Cos(Math.PI * fai2 / 180) * Math.Cos(Math.PI * fai / 180) }, { Math.Sin(Math.PI * fai1 / 180) * Math.Sin(Math.PI * fai / 180) } });
                var matrixD = DenseMatrix.OfArray(new[,] { { Math.Sin(Math.PI * fai / 180) * Math.Sin(Math.PI * fai2 / 180) }, { Math.Sin(Math.PI * fai / 180) * Math.Cos(Math.PI * fai2 / 180) }, { Math.Cos(Math.PI * fai / 180) } });

                //2个矩阵相乘，要注意矩阵乘法的维数要求
                var resultM = matrixA * matrixC;//也可以使用Multiply方法
                var resultN = matrixB * matrixD;//也可以使用Multiply方法
                double[][] resultM2 = resultM.ToRowArrays();//转换为double[][]数组
                double[] m = new double[resultM2.GetLength(0)];//转换为double[]数组
                double[][] resultN2 = resultN.ToRowArrays();//转换为double[][]数组
                double[] n = new double[resultN2.GetLength(0)];//转换为double[]数组

                for (int i = 0; i < resultM2.GetLength(0); i++)
                {
                    m[i] = resultM2[i][0];
                }

                for (int i = 0; i < resultM2.GetLength(0); i++)
                {
                    n[i] = resultN2[i][0];
                }
                MessageBox.Show("1");
                m = primecvt(m);
                n = primecvt(n);

                textBox7.Text = "(" + m[0].ToString() + "," + m[1].ToString() + "," + m[2].ToString() + "," + m[3].ToString() + ")" + "[" + n[0].ToString() + "," + n[1].ToString() + "," + n[2].ToString() + "," + n[3].ToString() + "]";


            }
            catch (Exception)
            {
                MessageBox.Show("Unexpected error");
            }

        }
    }
}
