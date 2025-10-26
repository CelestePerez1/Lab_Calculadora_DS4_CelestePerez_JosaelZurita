namespace Calculadora_CelestePerezJosaelZurita
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //centrear el form a penas se ejecuta
            this.StartPosition = FormStartPosition.CenterScreen;

            //Se asocian el keypress y texttchanged para validar numero decimal y caracteres no validos
            textMostrar.KeyPress += (s, e) => Validar.SoloNumeroDecimal(textMostrar, e);
            textMostrar.TextChanged += (s, e) => Validar.Sanitizar(textMostrar); 
        }

        //variable globales utilizadas para realizar las operaciones
        string operador = "";
        double num1 = 0;
        double num2 = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
        //no lo borre pq despues se corrompe el codigo :(
        }



        //boton para limpiar todo  el text box
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Validar.LimpiarTodo(textMostrar, ref num1, ref num2, ref operador);
        }

        //boton para borrar caracter por caracter 
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            Validar.BorrarCaracter(textMostrar);
        }

        //Configuración de los botones de los  números(0-9)
        // el botón  verifica si el TextMostrar tiene "0" al inicio.
        // Si es así, lo elimina antes de agregar el nuevo número.
        //esto se hace para cada numero de 0 al 9
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
        // Cada operador guarda el primer número (num1)  en operador y limpia el TextBox para ingresar num2.
        //Se realizan las validaciones en la clase validar y se llaman aca 
       
        private void btnSuma_Click(object sender, EventArgs e)
        {
            string op = "+";
            if (!Validar.OperadorVal(textMostrar, op))
            {
                operador = op;
                return;
            }

            operador = op;
            num1 = Convert.ToDouble(textMostrar.Text);
            textMostrar.Text = "0";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string op = "x";
            if (!Validar.OperadorVal(textMostrar, op))
            {
                operador = op;
                return;
            }

            operador = op;
            num1 = Convert.ToDouble(textMostrar.Text);
            textMostrar.Text = "0";
        }

        private void btnDivi_Click(object sender, EventArgs e)
        {
            string op = "÷";
            if (!Validar.OperadorVal(textMostrar, op))
            {
                operador = op;
                return;
            }

            operador = op;
            num1 = Convert.ToDouble(textMostrar.Text);
            textMostrar.Text = "0";
        }

        private void btnResta_Click(object sender, EventArgs e)
        {
            string op = "-";
            if (!Validar.OperadorVal(textMostrar, op))
            {
                operador = op;
                return;
            }

            operador = op;
            num1 = Convert.ToDouble(textMostrar.Text);
            textMostrar.Text = "0";
        }

        //Boton que ejecuta la operacion 
        private void btnIgual_Click(object sender, EventArgs e)
        {
            if (!Validar.OperadorVal(textMostrar)) return;

            num2 = Convert.ToDouble(textMostrar.Text);
            double resultado = 0;


            //Aqui se detecta que operacion se desea ejecutar 
            switch (operador)
            {
                case "+":
                    resultado = num1 + num2;
                    break;

                case "x":
                    resultado = num1 * num2;
                    break;
                case "÷":
                    resultado = num1 / num2;
                    break;
                case "-":
                    resultado = num1 - num2;
                    break;
            }

            //Validamos el resultado
            if (Validar.ResultValido(resultado))
                textMostrar.Text = resultado.ToString();
            else
                Validar.LimpiarTodo(textMostrar, ref num1, ref num2, ref operador);
        }

      
    }
}
