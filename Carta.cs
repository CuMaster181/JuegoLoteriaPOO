using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace JuegoLoteriaPOO
{
    internal class Carta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Image RutaImagen { get; set; }
        public string RutaAudio { get; set; }

        public Carta(int id, string nombre, Image rutaImagen, string rutaAudio)
        {
            Id = id;
            Nombre = nombre;
            RutaImagen = rutaImagen;
            RutaAudio = rutaAudio;
        }

        public static readonly Dictionary<int, Carta> cartas =
        new Dictionary<int, Carta>
        {
            {1,  new Carta(1,  "El Gallo",         Properties.Resources.El_Gallo,          "Audios/01.wav")},
            {2,  new Carta(2,  "El Diablito",      Properties.Resources.El_Diablito,       "Audios/02.wav")},
            {3,  new Carta(3,  "La Dama",          Properties.Resources.La_Dama,           "Audios/03.wav")},
            {4,  new Carta(4,  "El Catrín",        Properties.Resources.El_Catrin,         "Audios/04.wav")},
            {5,  new Carta(5,  "El Paraguas",      Properties.Resources.El_Paraguas,       "Audios/05.wav")},
            {6,  new Carta(6,  "La Sirena",        Properties.Resources.La_Sirena,         "Audios/06.wav")},
            {7,  new Carta(7,  "La Escalera",      Properties.Resources.La_Escalera,       "Audios/07.wav")},
            {8,  new Carta(8,  "La Botella",       Properties.Resources.La_Botella,        "Audios/08.wav")},
            {9,  new Carta(9,  "El Barril",        Properties.Resources.El_Barril,         "Audios/09.wav")},
            {10, new Carta(10, "El Árbol",         Properties.Resources.El_Arbol,          "Audios/10.wav")},
            {11, new Carta(11, "El Melón",         Properties.Resources.El_Melon,          "Audios/11.wav")},
            {12, new Carta(12, "El Valiente",      Properties.Resources.El_Valiente,       "Audios/12.wav")},
            {13, new Carta(13, "El Gorrito",       Properties.Resources.El_Gorrito,        "Audios/13.wav")},
            {14, new Carta(14, "La Muerte",        Properties.Resources.La_Muerte,         "Audios/14.wav")},
            {15, new Carta(15, "La Pera",          Properties.Resources.La_Pera,           "Audios/15.wav")},
            {16, new Carta(16, "La Bandera",       Properties.Resources.La_Bandera,        "Audios/16.wav")},
            {17, new Carta(17, "El Bandolón",      Properties.Resources.El_Bandolon,      "Audios/17.wav")},
            {18, new Carta(18, "El Violoncello",   Properties.Resources.El_Violoncello,   "Audios/18.wav")},
            {19, new Carta(19, "La Garza",         Properties.Resources.La_Garza,         "Audios/19.wav")},
            {20, new Carta(20, "El Pájaro",        Properties.Resources.El_Pajaro,        "Audios/20.wav")},
            {21, new Carta(21, "La Mano",          Properties.Resources.La_Mano,          "Audios/21.wav")},
            {22, new Carta(22, "La Bota",          Properties.Resources.La_Bota,          "Audios/22.wav")},
            {23, new Carta(23, "La Luna",          Properties.Resources.La_Luna,          "Audios/23.wav")},
            {24, new Carta(24, "El Cotorro",       Properties.Resources.El_Cotorro,       "Audios/24.wav")},
            {25, new Carta(25, "El Borracho",      Properties.Resources.El_Borracho,      "Audios/25.wav")},
            {26, new Carta(26, "El Negrito",       Properties.Resources.El_Negrito,       "Audios/26.wav")},
            {27, new Carta(27, "El Corazón",       Properties.Resources.El_Corazon,       "Audios/27.wav")},
            {28, new Carta(28, "La Sandía",        Properties.Resources.La_Sandia,        "Audios/28.wav")},
            {29, new Carta(29, "El Tambor",        Properties.Resources.El_Tambor,        "Audios/29.wav")},
            {30, new Carta(30, "El Camarón",       Properties.Resources.El_Camaron,       "Audios/30.wav")},
            {31, new Carta(31, "Las Jaras",        Properties.Resources.Las_Jaras,        "Audios/31.wav")},
            {32, new Carta(32, "El Músico",        Properties.Resources.El_Musico,        "Audios/32.wav")},
            {33, new Carta(33, "La Araña",         Properties.Resources.La_Araña,         "Audios/33.wav")},
            {34, new Carta(34, "El Soldado",       Properties.Resources.El_Soldado,       "Audios/34.wav")},
            {35, new Carta(35, "La Estrella",      Properties.Resources.La_Estrella,      "Audios/35.wav")},
            {36, new Carta(36, "El Cazo",          Properties.Resources.El_Cazo,          "Audios/36.wav")},
            {37, new Carta(37, "El Mundo",         Properties.Resources.El_Mundo,         "Audios/37.wav")},
            {38, new Carta(38, "El Apache",        Properties.Resources.El_Apache,        "Audios/38.wav")},
            {39, new Carta(39, "El Nopal",         Properties.Resources.El_Nopal,         "Audios/39.wav")},
            {40, new Carta(40, "El Alacrán",       Properties.Resources.El_Alacran,       "Audios/40.wav")},
            {41, new Carta(41, "La Rosa",          Properties.Resources.La_Rosa,          "Audios/41.wav")},
            {42, new Carta(42, "La Calavera",      Properties.Resources.La_Calavera,      "Audios/42.wav")},
            {43, new Carta(43, "La Campana",       Properties.Resources.La_Campana,       "Audios/43.wav")},
            {44, new Carta(44, "El Cantarito",     Properties.Resources.El_Cantarito,     "Audios/44.wav")},
            {45, new Carta(45, "El Venado",        Properties.Resources.El_Venado,        "Audios/45.wav")},
            {46, new Carta(46, "El Sol",           Properties.Resources.El_Sol,           "Audios/46.wav")},
            {47, new Carta(47, "La Corona",        Properties.Resources.La_Corona,        "Audios/47.wav")},
            {48, new Carta(48, "La Chalupa",       Properties.Resources.La_Chalupa,       "Audios/48.wav")},
            {49, new Carta(49, "El Pino",          Properties.Resources.El_Pino,          "Audios/49.wav")},
            {50, new Carta(50, "El Pescado",       Properties.Resources.El_Pescado,       "Audios/50.wav")},
            {51, new Carta(51, "La Palma",         Properties.Resources.La_Palma,         "Audios/51.wav")},
            {52, new Carta(52, "La Maceta",        Properties.Resources.La_Maceta,        "Audios/52.wav")},
            {53, new Carta(53, "El Arpa",          Properties.Resources.El_Arpa,          "Audios/53.wav")},
            {54, new Carta(54, "La Rana",          Properties.Resources.La_Rana,          "Audios/54.wav")}
        };
    }
}
