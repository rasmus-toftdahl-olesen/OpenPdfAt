using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace OpenPdfAt
{
   public class Program
   {
      public static int Main(string[] args)
      {
         IPdfHandler handler = null;
         try
         {
            RegistryKey pdfKey = Registry.ClassesRoot.OpenSubKey(".pdf");
            string pdfDefaultHandler = pdfKey.GetValue("") as string;
            RegistryKey handlerKey = Registry.ClassesRoot.OpenSubKey(pdfDefaultHandler);
            RegistryKey handlerShell = handlerKey.OpenSubKey("shell");
            RegistryKey handlerOpen = handlerShell.OpenSubKey("open");
            RegistryKey handlerCommand = handlerOpen.OpenSubKey("command");
            string executable = handlerCommand.GetValue("") as string;
            if (executable.EndsWith(" \"%1\""))
            {
               executable = executable.Substring(0, executable.Length - " \"%1\"".Length);
            }
            if (executable.ToLower().Contains("adobe"))
            {
               handler = new AcrobatReaderPdfHandler(executable);
            }
         }
         catch
         {
            handler = new DefaultPdfHandler();
         }

         if (args.Length == 3)
         {
            string action = args[0];
            string position = args[1];
            string filename = args[2];

            if ( !File.Exists(filename) )
            {
               Console.WriteLine("File does not exist: {0}", filename);
               return -2;
            }
            switch (action)
            {
               case "page":
                  int page;
                  if (Int32.TryParse(position, out page))
                  {
                     return handler.OpenAtPage(page, filename);
                  }
                  break;

               case "nameddest":
                  return handler.OpenAtNamedDest(position, filename);
            }
         }

         Console.WriteLine("Invalid arguments, OpenPDF@ always takes three arguments:");
         Console.WriteLine();
         Console.WriteLine("<action> <position> <filename.pdf>");
         Console.WriteLine();
         Console.WriteLine("Where <action> and <position> can be:");
         Console.WriteLine();
         Console.WriteLine("page 45");
         Console.WriteLine("     Open the PDF at page 45.");
         Console.WriteLine();
         Console.WriteLine("nameddest the-mad-hatter");
         Console.WriteLine("     Open the PDF at the destination named the-mad-hatter");
         return -1;
      }
   }
}
