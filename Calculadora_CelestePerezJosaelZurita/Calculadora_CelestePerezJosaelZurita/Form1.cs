namespace Calculadora_CelestePerezJosaelZurita
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //variable
        string operador = "";
        double num1 = 0;
        double num2 = 0;



        private void Form1_Load(object sender, EventArgs e)
        {

        }



        //boton para limpiar todo  el text box
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            textMostrar.Text = "0";
            num1 = 0;
            num2 = 0;
            operador = "";
        }

        //boton para borrar caracter por caracter 
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (textMostrar.Text.Length == 1)
            {
                textMostrar.Text = "0";
            }
            else
            {
                textMostrar.Text = textMostrar.Text.Substring(0, textMostrar.Text.Length - 1);
            }
        }

        //Configuración de los botones de los  números
        private void btn1_Click(object sender, EventArgs e)
        {
            if (textMostrar.Text == "0")
            {
                textMostrar.Text = "";
            }
            textMostrar.Text = textMostrar.Text + "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (textMostrar.Text == "0")
            {
                textMostrar.Text = "";
            }
            textMostrar.Text = textMostrar.Text + "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (textMostrar.Text == "0")
            {
                textMostrar.Text = "";
            }
            textMostrar.Text = textMostrar.Text + "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (textMostrar.Text == "0")
            {
                textMostrar.Text = "";
            }
            textMostrar.Text = textMostrar.Text + "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (textMostrar.Text == "0")
            {
                textMostrar.Text = "";
            }
            textMostrar.Text = textMostrar.Text + "5";
        }


        private void btn6_Click(object sender, EventArgs e)
        {
            if (textMostrar.Text == "0")
            {
                textMostrar.Text = "";
            }
            textMostrar.Text = textMostrar.Text + "6";
        }


        private void btn7_Click(object sender, EventArgs e)
        {
            if (textMostrar.Text == "0")
            {
                textMostrar.Text = "";
            }
            textMostrar.Text = textMostrar.Text + "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (textMostrar.Text == "0")
            {
                textMostrar.Text = "";
            }
            textMostrar.Text = textMostrar.Text + "8";
        }


        private void button9_Click(object sender, EventArgs e)
        {
            if (textMostrar.Text == "0")
            {
                textMostrar.Text = "";
            }
            textMostrar.Text = textMostrar.Text + "9";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            textMostrar.Text = textMostrar.Text + "0";
        }

        private void btnPunto_Click(object sender, EventArgs e)
        {
            textMostrar.Text = textMostrar.Text + ".";
        }

        //Configuración de los operadores 
        private void btnSuma_Click(object sender, EventArgs e)
        {
            operador = "+";
            num1 = Convert.ToDouble(textMostrar.Text);
            textMostrar.Text = "0";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            operador = "x";
            num1 = Convert.ToDouble(textMostrar.Text);
            textMostrar.Text = "0";
        }

        private void btnIgual_Click(object sender, EventArgs e)
        {
            num2 = Convert.ToDouble(textMostrar.Text);

            switch (operador)
            {
                case "+":
                    textMostrar.Text = $"{num1+num2}";
                    break;

                case "x":
                    textMostrar.Text = $"{num1 * num2}";
                    break;
            }
        }
    }
}
