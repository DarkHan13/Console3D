using System.Drawing;
using System.Runtime.InteropServices;

namespace ConsoleRender.ConsoleEngine;

public static class Kernel32
{
    // Импорт функции для получения информации о шрифте консоли
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GetCurrentConsoleFont(
        IntPtr hConsoleOutput,
        bool bMaximumWindow,
        out CONSOLE_FONT_INFO lpConsoleCurrentFont);

    [StructLayout(LayoutKind.Sequential)]
    private struct CONSOLE_FONT_INFO
    {
        public int nFont;
        public Coord dwFontSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct Coord
    {
        public short X;
        public short Y;

        public Coord(short x, short y)
        {
            X = x;
            Y = y;
        }
    }

    const int STD_OUTPUT_HANDLE = -11;

    public static Size GetConsoleFontSize()
    {
        IntPtr hnd = GetStdHandle(STD_OUTPUT_HANDLE);
        CONSOLE_FONT_INFO cfi;
        
        if (GetCurrentConsoleFont(hnd, false, out cfi))
        {
            return new Size
            {
                Width = cfi.dwFontSize.X,
                Height = cfi.dwFontSize.Y
            };
        }
        else
        {
            // Если не удалось получить размеры, вернем стандартные размеры (8x16)
            return new Size
            {
                Width = 8,
                Height = 16
            };
        }
    }
}