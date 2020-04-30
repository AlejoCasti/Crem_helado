using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Lógica
{
    public class Validaciones
    {
        public void Números(KeyPressEventArgs pE)
        {
            if (char.IsDigit(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else if (char.IsControl(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else if (pE.KeyChar == 32)
            {
                pE.Handled = true;
            }
            else
            {
                pE.Handled = true;
                MessageBox.Show("Por favor ingrese solo números");
            }


        }
        public void Cuentas(KeyPressEventArgs pE)
        {
            if (Char.IsLetter(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else if (Char.IsControl(pE.KeyChar))
            {
                pE.Handled = false;
            }

            else if (pE.KeyChar == 32)
            {
                pE.Handled = true;
            }
            else
            {
                pE.Handled = false;
            }
        }
        public void Nombres(KeyPressEventArgs pE)
        {
            if (Char.IsLetter(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else if (Char.IsControl(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else if (Char.IsSeparator(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else
            {
                pE.Handled = true;
            }
        }
        public void Pass(KeyPressEventArgs pE)
        {
            if (Char.IsLetter(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else if (Char.IsControl(pE.KeyChar))
            {
                pE.Handled = false;
            }
            else if (pE.KeyChar == 32)
            {
                pE.Handled = true;

            }
            else
            {
                pE.Handled = false;
            }

        }
        public bool ValidarEmail(string Correo)
        {
            string strModel = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (System.Text.RegularExpressions.Regex.IsMatch(Correo, strModel))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
