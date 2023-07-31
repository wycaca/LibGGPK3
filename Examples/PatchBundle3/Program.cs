using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace PatchBundle3 {
	public class Program {
		public static void Main(string[] args) {
            var zipPath = "patch.zip";
            try
            {
				var version = Assembly.GetExecutingAssembly().GetName().Version!;
				Console.WriteLine($"PatchBundle3 (v{version.Major}.{version.Minor}.{version.Build})  Copyright (C) 2022 aianlinb"); // ©
				Console.WriteLine();
				if (args.Length == 0)
				{
					args = new string[2];
					Console.Write("Path To _.index.bin: ");
					args[0] = Console.ReadLine()!;
					Console.Write("Path to zip file: ");
					args[1] = Console.ReadLine()!;
				}
				else if (args.Length != 2)
				{
					Console.WriteLine("Usage: PatchBundledGGPK3 <PathToIndexBin> <ZipFile>");
					Console.WriteLine();
					Console.WriteLine("Enter to exit . . .");
					Console.ReadLine();
					return;
				}
				if (!File.Exists(args[0]))
				{
					Console.WriteLine("FileNotFound: " + args[0]);
					Console.WriteLine();
					Console.WriteLine("Enter to exit . . .");
					Console.ReadLine();
					return;
				}
				if (!Directory.Exists(args[1]) && !File.Exists(args[1]))
				{
					Console.WriteLine("FileNotFound: " + args[1]);
					Console.WriteLine();
					Console.WriteLine("Enter to exit . . .");
					Console.ReadLine();
					return;
				}

				Console.WriteLine("Index file: " + args[0]);
				Console.WriteLine("Patch file: " + args[1]);
				Console.WriteLine("读取 index file . . .");
				var index = new LibBundle3.Index(args[0], false);
				Console.WriteLine("创建压缩文件...");
				ZipFile.CreateFromDirectory(args[1], zipPath);

				Console.WriteLine("替换文件中 . . .");
				var zip = ZipFile.OpenRead(zipPath);

				index.Replace(zip.Entries);
				index.Dispose();
				zip.Dispose();
                Console.WriteLine("文件替换成功!");
			}
			catch (Exception e)
			{
				Console.WriteLine("文件替换失败!");
				Console.Error.WriteLine(e);
			}
			finally {
                Console.WriteLine("清理压缩文件...");
                File.Delete(zipPath);
            }
            Console.WriteLine();
			Console.WriteLine("Enter to exit . . .");
			Console.ReadLine();
		}
	}
}