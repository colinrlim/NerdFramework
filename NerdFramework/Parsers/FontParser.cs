﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NerdFramework
{
    public static class FontParser
    {
        public static Font FromFile(string fileLocation)
        {
            /* Font (TTF) Specifications
             * http://paulbourke.net/dataformats/mtl/
             */
            var fs = new FileStream(@fileLocation, FileMode.Open);
            /*var len = (int)fs.Length;
            var bits = new byte[len];
            fs.Read(bits, 0, len);
            // Dump 16 bytes per line
            for (int ix = 0; ix < len; ix += 16)
            {
                var cnt = Math.Min(16, len - ix);
                var line = new byte[cnt];
                Array.Copy(bits, ix, line, 0, cnt);
                // Write address + hex + ascii
                Console.Write("{0:X6}  ", ix);
                Console.Write(BitConverter.ToString(line));
                Console.Write("  ");
                // Convert non-ascii characters to .
                for (int jx = 0; jx < cnt; ++jx)
                    if (line[jx] < 0x20 || line[jx] > 0x7f) line[jx] = (byte)'.';
                Console.WriteLine(Encoding.ASCII.GetString(line));
            }
            Console.ReadLine();*/

            throw new System.NotImplementedException();
        }

        public static void FromFileDraw(string fileLocation, Color3[,] screen, string text, Vector2i position, int pixelHeight)
        {

        }
    }
}
