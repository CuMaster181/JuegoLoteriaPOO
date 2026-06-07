using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Reflection;
using System.IO;
using Microsoft.VisualBasic.Devices;

namespace JuegoLoteriaPOO
{
    public class Carta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Image RutaImagen { get; set; }
        public UnmanagedMemoryStream audio { get; set; }

        public Carta(int id, string nombre, Image rutaImagen, UnmanagedMemoryStream audio)
        {
            Id = id;
            Nombre = nombre;
            RutaImagen = rutaImagen;
            this.audio = audio;
        }

        public static readonly Dictionary<int, Carta> cartas = new Dictionary<int, Carta>
        {
            {1,  new Carta(1,  "El Gallo",         Properties.Resources.El_Gallo,         Properties.Resources.ElGallo)},
            {2,  new Carta(2,  "El Diablito",      Properties.Resources.El_Diablito,      Properties.Resources.ElDiablito)},
            {3,  new Carta(3,  "La Dama",          Properties.Resources.La_Dama,          Properties.Resources.LaDama)},
            {4,  new Carta(4,  "El Catrín",        Properties.Resources.El_Catrin,        Properties.Resources.ElCatrin)},
            {5,  new Carta(5,  "El Paraguas",      Properties.Resources.El_Paraguas,      Properties.Resources.ElParaguas)},
            {6,  new Carta(6,  "La Sirena",        Properties.Resources.La_Sirena,        Properties.Resources.LaSirena)},
            {7,  new Carta(7,  "La Escalera",      Properties.Resources.La_Escalera,      Properties.Resources.LaEscalera)},
            {8,  new Carta(8,  "La Botella",       Properties.Resources.La_Botella,       Properties.Resources.LaBotella)},
            {9,  new Carta(9,  "El Barril",        Properties.Resources.El_Barril,        Properties.Resources.ElBarril)},
            {10, new Carta(10, "El Árbol",         Properties.Resources.El_Arbol,         Properties.Resources.ElArbol)},
            {11, new Carta(11, "El Melón",         Properties.Resources.El_Melon,         Properties.Resources.ElMelon)},
            {12, new Carta(12, "El Valiente",      Properties.Resources.El_Valiente,      Properties.Resources.ElValiente)},
            {13, new Carta(13, "El Gorrito",       Properties.Resources.El_Gorrito,       Properties.Resources.ElGorrito)},
            {14, new Carta(14, "La Muerte",        Properties.Resources.La_Muerte,        Properties.Resources.LaMuerte)},
            {15, new Carta(15, "La Pera",          Properties.Resources.La_Pera,          Properties.Resources.LaPera)},
            {16, new Carta(16, "La Bandera",       Properties.Resources.La_Bandera,       Properties.Resources.LaBandera)},
            {17, new Carta(17, "El Bandolón",      Properties.Resources.El_Bandolon,      Properties.Resources.ElBandolon)},
            {18, new Carta(18, "El Violoncello",   Properties.Resources.El_Violoncello,   Properties.Resources.ElVioloncello)},
            {19, new Carta(19, "La Garza",         Properties.Resources.La_Garza,         Properties.Resources.LaGarza)},
            {20, new Carta(20, "El Pájaro",        Properties.Resources.El_Pajaro,        Properties.Resources.ElPajaro)},
            {21, new Carta(21, "La Mano",          Properties.Resources.La_Mano,          Properties.Resources.LaMano)},
            {22, new Carta(22, "La Bota",          Properties.Resources.La_Bota,          Properties.Resources.LaBota)},
            {23, new Carta(23, "La Luna",          Properties.Resources.La_Luna,          Properties.Resources.LaLuna)},
            {24, new Carta(24, "El Cotorro",       Properties.Resources.El_Cotorro,       Properties.Resources.ElCotorro)},
            {25, new Carta(25, "El Borracho",      Properties.Resources.El_Borracho,      Properties.Resources.ElBorracho)},
            {26, new Carta(26, "El Negrito",       Properties.Resources.El_Negrito,       Properties.Resources.ElNegrito)},
            {27, new Carta(27, "El Corazón",       Properties.Resources.El_Corazon,       Properties.Resources.ElCorazon)},
            {28, new Carta(28, "La Sandía",        Properties.Resources.La_Sandia,        Properties.Resources.LaSandia)},
            {29, new Carta(29, "El Tambor",        Properties.Resources.El_Tambor,        Properties.Resources.ElTambor)},
            {30, new Carta(30, "El Camarón",       Properties.Resources.El_Camaron,       Properties.Resources.ElCamaron)},
            {31, new Carta(31, "Las Jaras",        Properties.Resources.Las_Jaras,        Properties.Resources.LasJaras)},
            {32, new Carta(32, "El Músico",        Properties.Resources.El_Musico,        Properties.Resources.ElMusico)},
            {33, new Carta(33, "La Araña",         Properties.Resources.La_Araña,         Properties.Resources.LaAraña)},
            {34, new Carta(34, "El Soldado",       Properties.Resources.El_Soldado,       Properties.Resources.ElSoldado)},
            {35, new Carta(35, "La Estrella",      Properties.Resources.La_Estrella,      Properties.Resources.LaEstrella)},
            {36, new Carta(36, "El Cazo",          Properties.Resources.El_Cazo,          Properties.Resources.ElCazo)},
            {37, new Carta(37, "El Mundo",         Properties.Resources.El_Mundo,         Properties.Resources.ElMundo)},
            {38, new Carta(38, "El Apache",        Properties.Resources.El_Apache,        Properties.Resources.ElApache)},
            {39, new Carta(39, "El Nopal",         Properties.Resources.El_Nopal,         Properties.Resources.ElNopal)},
            {40, new Carta(40, "El Alacrán",       Properties.Resources.El_Alacran,       Properties.Resources.ElAlacran)},
            {41, new Carta(41, "La Rosa",          Properties.Resources.La_Rosa,          Properties.Resources.LaRosa)},
            {42, new Carta(42, "La Calavera",      Properties.Resources.La_Calavera,      Properties.Resources.LaCalavera)},
            {43, new Carta(43, "La Campana",       Properties.Resources.La_Campana,       Properties.Resources.LaCampana)},
            {44, new Carta(44, "El Cantarito",     Properties.Resources.El_Cantarito,     Properties.Resources.ElCantarito)},
            {45, new Carta(45, "El Venado",        Properties.Resources.El_Venado,        Properties.Resources.ElVenado)},
            {46, new Carta(46, "El Sol",           Properties.Resources.El_Sol,           Properties.Resources.ElSol)},
            {47, new Carta(47, "La Corona",        Properties.Resources.La_Corona,        Properties.Resources.LaCorona)},
            {48, new Carta(48, "La Chalupa",       Properties.Resources.La_Chalupa,       Properties.Resources.LaChalupa)},
            {49, new Carta(49, "El Pino",          Properties.Resources.El_Pino,          Properties.Resources.ElPino)},
            {50, new Carta(50, "El Pescado",       Properties.Resources.El_Pescado,       Properties.Resources.ElPescado)},
            {51, new Carta(51, "La Palma",         Properties.Resources.La_Palma,         Properties.Resources.LaPalma)},
            {52, new Carta(52, "La Maceta",        Properties.Resources.La_Maceta,        Properties.Resources.LaMaceta)},
            {53, new Carta(53, "El Arpa",          Properties.Resources.El_Arpa,          Properties.Resources.ElArpa)},
            {54, new Carta(54, "La Rana",          Properties.Resources.La_Rana,          Properties.Resources.LaRana)}
        };
    }
}
