using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora_CelestePerezJosaelZurita
{
    internal class Validar 
    {
        private const int longiMax = 12;
        private static readonly char[] operadores = new char[] { '+', '-', 'x', 'X', '*', '/', '÷' };
        private static readonly char[] decimalesAceptados = new char[] { '.', Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) };

        //Validaciones
        public static void SoloNumeroDecimal(TextBox textBox, KeyPressEventArgs e)
        {

            char key = e.KeyChar;

            if (key == (char)Keys.Back) return;

            // If digit
            if (char.IsDigit(key))
            {
                // Prevent exceeding max length
                if (textBox.Text.Length >= longiMax)
                {
                    e.Handled = true;
                }
                return;
            }

            if (decimalesAceptados.Contains(key))
            {
                // Determinar último número: substring después del último operador
                int lastOpIdx = LastOperatorIndex(textBox.Text);
                string ultimoNum = lastOpIdx >= 0 ? textBox.Text.Substring(lastOpIdx + 1) : textBox.Text;

                // Si el número ya tiene un separador decimal, bloquear
                if (ultimoNum.IndexOfAny(decimalesAceptados) >= 0)
                {
                    e.Handled = true;
                    return;
                }

                // Si el número está vacío, insertar '0' 
                int espacioNecesario = 1; // para el '.' que se añadirá
                if (ultimoNum.Length == 0)
                    espacioNecesario += 1; // por el '0' que vamos a insertar

                if (textBox.Text.Length + espacioNecesario > longiMax)
                {
                    e.Handled = true;
                    return;
                }

                if (ultimoNum.Length == 0)
                {
                    // insertar 0 antes del separador y dejar que el char actual (.) se agregue normalmente
                    textBox.Text += "0";
                    textBox.SelectionStart = textBox.Text.Length;
                }

                // permitir que el '.' 
                return;
            }

            // Si es operador permitido
            if (operadores.Contains(key))
            {
                // No permitir que el primer caracter sea operador
                if (string.IsNullOrEmpty(textBox.Text) || textBox.Text == "0")
                {
                    e.Handled = true;
                    return;
                }

                // Si el último caracter ya es un operador -> reemplazarlo por el nuevo
                char ultimo = textBox.Text[textBox.Text.Length - 1];
                if (operadores.Contains(ultimo))
                {
                    // Reemplazar último operador por el nuevo
                    textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1) + key;
                    textBox.SelectionStart = textBox.Text.Length;
                    e.Handled = true; // ya manejamos la inserción
                    return;
                }

                // Antes de añadir, verificar longitud máxima
                if (textBox.Text.Length >= longiMax)
                {
                    e.Handled = true;
                    return;
                }

                // permitir operador
                return;
            }

            // Cualquier otro carácter lo bloquea
            e.Handled = true;



        } //termina primer val


        //índice del último operador en la cadena, o -1 si no hay
        private static int LastOperatorIndex(string s)
        {
            if (string.IsNullOrEmpty(s)) return -1;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (operadores.Contains(s[i])) return i;
            }
            return -1;
        }


        // Valida la entrada ante operaciones no cubiertas por 
        public static void Sanitizar(TextBox textBox)
        {
            string txt = textBox.Text ?? "";
            if (txt.Length == 0) return;

            //Quitar caracteres no permitidos
            var allowed = txt.Where(c => char.IsDigit(c) || operadores.Contains(c) || decimalesAceptados.Contains(c)).ToArray();
            string limpio = new string(allowed);

            //Reemplazar multiplicadores/divisiones
            limpio = limpio.Replace('*', 'x').Replace('X', 'x').Replace('/', '÷');

            //Validar operadores consecutivos
            for (int i = 1; i < limpio.Length;)
            {
                if (operadores.Contains(limpio[i]) && operadores.Contains(limpio[i - 1]))
                {
                    // eliminar el operador anterior (mantener el actual)
                    limpio = limpio.Remove(i - 1, 1);
                    // no incrementar i 
                }
                else i++;
            }

            // Asegurar que cada número tenga un separador decimal
            string reconstruido = "";
            bool inNumber = false;
            bool decimalEnNumero = false;
            foreach (char c in limpio)
            {
                if (operadores.Contains(c))
                {
                    // operador: resetear flags
                    reconstruido += c;
                    inNumber = false;
                    decimalEnNumero = false;
                }
                else if (char.IsDigit(c))
                {
                    reconstruido += c;
                    inNumber = true;
                }
                else if (decimalesAceptados.Contains(c))
                {
                    if (!inNumber)
                    {
                        // Si empezamos número con '.', anteponer '0'
                        if (reconstruido.Length + 2 <= longiMax) 
                        {
                            reconstruido += '0';
                            reconstruido += c; // añadir decimal
                            inNumber = true;
                            decimalEnNumero = true;
                        }
                        else
                        {
                            // no hay espacio, ignorar
                        }
                    }
                    else
                    {
                        if (!decimalEnNumero)
                        {
                            reconstruido += c;
                            decimalEnNumero = true;
                        }
                        // si ya hay decimal, ignorar este caracter
                    }
                }
            }

            //longitud máxima
            if (reconstruido.Length > longiMax)
                reconstruido = reconstruido.Substring(0, longiMax);

            //Si queda vacío, colocar "0"
            if (string.IsNullOrEmpty(reconstruido))
                reconstruido = "0";

            // Si cambió, actualizar TextBox sin mover el cursor al final inesperadamente
            if (reconstruido != textBox.Text)
            {
                int oldSelection = textBox.SelectionStart;
                textBox.Text = reconstruido;
                textBox.SelectionStart = Math.Min(oldSelection, textBox.Text.Length);
            }
        }















        //Evitar operadores consecutivos 
        // Sobrecarga antigua: mantiene compatibilidad con llamadas sin nuevoOperador.
        // Si se llama sin nuevoOperador simplemente devuelve true/false (comportamiento original).
        public static bool OperadorVal(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text) || textBox.Text == "0")
                return false;

            char ultimo = textBox.Text[textBox.Text.Length - 1];
            return !operadores.Contains(ultimo);
        }

        // Nueva sobrecarga: recibe el operador que se desea insertar.
        // Si el último carácter es operador lo reemplaza por el nuevo y devuelve false
        // (porque ya hizo el reemplazo y no se necesita insertar de nuevo desde el caller).
        public static bool OperadorVal(TextBox textBox, string nuevoOperador)
        {
            if (string.IsNullOrEmpty(textBox.Text) || textBox.Text == "0")
                return false;

            char ultimo = textBox.Text[textBox.Text.Length - 1];
            if (operadores.Contains(ultimo))
            {
                // Reemplaza el operador anterior con el nuevo
                textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1) + nuevoOperador;
                textBox.SelectionStart = textBox.Text.Length;
                return false; // ya manejado (reemplazado)
            }

            return true; // OK para insertar el operador normalmente
        }


        //Evitar resultados indefinitos 
        public static bool ResultValido(double resultado)
        {
            if (double.IsNaN(resultado) || double.IsInfinity(resultado)) 
            {
                MessageBox.Show("Resultado indefinido o finito.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //Validacion de campos vacios o iniciar con 0
        public static bool CampoVacio(TextBox textBox)
        {
            return string.IsNullOrEmpty(textBox.Text) || textBox.Text == "0";
        }

        //Borrar Caracter
        public static void BorrarCaracter(TextBox textBox)
        {
            if (textBox.Text.Length == 1)
            {
                textBox.Text = "0";
            }
            else
            {
                textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
            }
        }

        //Limpiar todo
        public static void LimpiarTodo(TextBox textBox, ref double num1, ref double num2, ref string operador)
        {
            textBox.Text = "0";
            num1 = 0;
            num2 = 0;
            operador = "";
        }


    }
}
