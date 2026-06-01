using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Reflection;

namespace JuegoLoteriaPOO
{
    internal class Carta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Image RutaImagen { get; set; }

        public Carta(int id, string nombre, Image rutaImagen)
        {
            Id = id;
            Nombre = nombre;
            RutaImagen = rutaImagen;
        }

        public static readonly Dictionary<int, Carta> cartas = new Dictionary<int, Carta>
        {
            {1,  new Carta(1,  "El Gallo",         Properties.Resources.El_Gallo)},
            {2,  new Carta(2,  "El Diablito",      Properties.Resources.El_Diablito)},
            {3,  new Carta(3,  "La Dama",          Properties.Resources.La_Dama)},
            {4,  new Carta(4,  "El Catrín",        Properties.Resources.El_Catrin)},
            {5,  new Carta(5,  "El Paraguas",      Properties.Resources.El_Paraguas)},
            {6,  new Carta(6,  "La Sirena",        Properties.Resources.La_Sirena)},
            {7,  new Carta(7,  "La Escalera",      Properties.Resources.La_Escalera)},
            {8,  new Carta(8,  "La Botella",       Properties.Resources.La_Botella)},
            {9,  new Carta(9,  "El Barril",        Properties.Resources.El_Barril)},
            {10, new Carta(10, "El Árbol",         Properties.Resources.El_Arbol)},
            {11, new Carta(11, "El Melón",         Properties.Resources.El_Melon)},
            {12, new Carta(12, "El Valiente",      Properties.Resources.El_Valiente)},
            {13, new Carta(13, "El Gorrito",       Properties.Resources.El_Gorrito)},
            {14, new Carta(14, "La Muerte",        Properties.Resources.La_Muerte)},
            {15, new Carta(15, "La Pera",          Properties.Resources.La_Pera)},
            {16, new Carta(16, "La Bandera",       Properties.Resources.La_Bandera)},
            {17, new Carta(17, "El Bandolón",      Properties.Resources.El_Bandolon)},
            {18, new Carta(18, "El Violoncello",   Properties.Resources.El_Violoncello)},
            {19, new Carta(19, "La Garza",         Properties.Resources.La_Garza)},
            {20, new Carta(20, "El Pájaro",        Properties.Resources.El_Pajaro)},
            {21, new Carta(21, "La Mano",          Properties.Resources.La_Mano)},
            {22, new Carta(22, "La Bota",          Properties.Resources.La_Bota)},
            {23, new Carta(23, "La Luna",          Properties.Resources.La_Luna)},
            {24, new Carta(24, "El Cotorro",       Properties.Resources.El_Cotorro)},
            {25, new Carta(25, "El Borracho",      Properties.Resources.El_Borracho)},
            {26, new Carta(26, "El Negrito",       Properties.Resources.El_Negrito)},
            {27, new Carta(27, "El Corazón",       Properties.Resources.El_Corazon)},
            {28, new Carta(28, "La Sandía",        Properties.Resources.La_Sandia)},
            {29, new Carta(29, "El Tambor",        Properties.Resources.El_Tambor)},
            {30, new Carta(30, "El Camarón",       Properties.Resources.El_Camaron)},
            {31, new Carta(31, "Las Jaras",        Properties.Resources.Las_Jaras)},
            {32, new Carta(32, "El Músico",        Properties.Resources.El_Musico)},
            {33, new Carta(33, "La Araña",         Properties.Resources.La_Araña)},
            {34, new Carta(34, "El Soldado",       Properties.Resources.El_Soldado)},
            {35, new Carta(35, "La Estrella",      Properties.Resources.La_Estrella)},
            {36, new Carta(36, "El Cazo",          Properties.Resources.El_Cazo)},
            {37, new Carta(37, "El Mundo",         Properties.Resources.El_Mundo)},
            {38, new Carta(38, "El Apache",        Properties.Resources.El_Apache)},
            {39, new Carta(39, "El Nopal",         Properties.Resources.El_Nopal)},
            {40, new Carta(40, "El Alacrán",       Properties.Resources.El_Alacran)},
            {41, new Carta(41, "La Rosa",          Properties.Resources.La_Rosa)},
            {42, new Carta(42, "La Calavera",      Properties.Resources.La_Calavera)},
            {43, new Carta(43, "La Campana",       Properties.Resources.La_Campana)},
            {44, new Carta(44, "El Cantarito",     Properties.Resources.El_Cantarito)},
            {45, new Carta(45, "El Venado",        Properties.Resources.El_Venado)},
            {46, new Carta(46, "El Sol",           Properties.Resources.El_Sol)},
            {47, new Carta(47, "La Corona",        Properties.Resources.La_Corona)},
            {48, new Carta(48, "La Chalupa",       Properties.Resources.La_Chalupa)},
            {49, new Carta(49, "El Pino",          Properties.Resources.El_Pino)},
            {50, new Carta(50, "El Pescado",       Properties.Resources.El_Pescado)},
            {51, new Carta(51, "La Palma",         Properties.Resources.La_Palma)},
            {52, new Carta(52, "La Maceta",        Properties.Resources.La_Maceta)},
            {53, new Carta(53, "El Arpa",          Properties.Resources.El_Arpa)},
            {54, new Carta(54, "La Rana",          Properties.Resources.La_Rana)}
        };
    }
}
