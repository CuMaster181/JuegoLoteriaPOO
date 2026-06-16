# 🎴 Juego de Lotería Mexicana — POO

Implementación digital de la **Lotería Mexicana** tradicional, desarrollada en **C# con Windows Forms** como proyecto de Programación Orientada a Objetos. Incluye modo solitario y multijugador en red local.

---

## 📸 Características

- 🃏 **Baraja completa** de 54 cartas de Lotería con imagen y audio original por carta
- 🎯 **10 formas de ganar** configurables: línea horizontal, vertical, diagonal, cruz, T, L, J, pollita y tabla completa
- 👤 **Modo solitario** con 1 a 4 tablas simultáneas
- 🌐 **Modo multijugador** en red local (TCP), con roles de Host y Cliente
- 💬 **Chat en tiempo real** entre jugadores durante la partida
- 💾 **Tablas personalizadas** que se pueden guardar y reutilizar
- 🏆 Sistema de **puntaje** y manejo de **desempates**

---

## 🚀 Cómo ejecutar

### Opción A — Ejecutable precompilado

1. Descarga la carpeta `bin/Release/net8.0-windows/win-x64/`
2. Ejecuta `JuegoLoteriaPOO.exe`

> **Requisito:** Windows 10/11 x64. No necesita instalar .NET por separado (publicación self-contained).

### Opción B — Compilar desde código fuente

**Requisitos:**
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 o superior (con soporte para Windows Forms)

```bash
git clone https://github.com/tu-usuario/JuegoLoteriaPOO.git
cd JuegoLoteriaPOO
dotnet build
dotnet run
```

---

## 🎮 Cómo jugar

### Modo Solitario
1. Abre el juego y selecciona **Solitario**
2. Elige tu nombre y cuántas tablas quieres usar (1–4)
3. Crea o personaliza tu tabla de lotería
4. ¡Marca las figuras conforme el "gritón" va cantando las cartas!

### Modo Multijugador (Red Local)
1. Un jugador actúa como **Host**: elige un puerto y espera conexiones
2. Los demás jugadores eligen **Unirse** e ingresan la IP y puerto del host
3. El host configura las reglas (figuras habilitadas, número de tablas)
4. Cuando todos estén listos, el host inicia la partida

---

## 🏆 Formas de ganar

| Figura | Descripción |
|---|---|
| Horizontal | Una fila completa |
| Vertical | Una columna completa |
| Diagonal Principal | De esquina superior izquierda a inferior derecha |
| Diagonal Secundaria | De esquina superior derecha a inferior izquierda |
| Cruzita | Cruz centrada en la casilla del medio |
| Pollita | Las cuatro esquinas más el centro |
| T | Forma de T |
| L | Forma de L |
| J | Forma de J |
| Tabla Completa | Todas las casillas marcadas |

El host puede habilitar o deshabilitar cualquier combinación antes de iniciar la partida.

---

## 🗂️ Estructura del proyecto

```
JuegoLoteriaPOO/
├── Carta.cs                  # Modelo de carta (imagen + audio)
├── GeneradorCartas.cs        # Creación del mazo
├── GeneradorTablas.cs        # Generación aleatoria de tablas
├── TablaJugador.cs           # Tabla 4x4 de cada jugador
├── VerificadorDeVictoria.cs  # Lógica de verificación de patrones
├── PatronVictoria.cs         # Definición de las figuras ganadoras
├── TipoVictoria.cs           # Enum de formas de ganar
├── GestorPartida.cs          # Control del flujo del juego
├── GestorMultijugador.cs     # Comunicación TCP entre jugadores
├── GestorPuntaje.cs          # Sistema de puntaje
├── ConfiguracionPartida.cs   # Ajustes de la sesión
├── FormMenuPrincipal.cs      # Menú principal
├── FormPartida.cs            # Pantalla de juego
├── FormCrearTabla.cs         # Editor de tablas
└── Resources/                # Imágenes (.jpg) y audios (.wav) de las 54 cartas
```

---

## 🛠️ Tecnologías

- **Lenguaje:** C# (.NET 8)
- **UI:** Windows Forms
- **Red:** TCP/IP (`TcpListener` / `TcpClient`)
- **Persistencia:** JSON (tablas guardadas)

---

## 📋 Requisitos del sistema

| | Mínimo |
|---|---|
| SO | Windows 10 x64 |
| RAM | 256 MB |
| Almacenamiento | ~150 MB |
| Red (multijugador) | Red local (LAN / mismo router) |
