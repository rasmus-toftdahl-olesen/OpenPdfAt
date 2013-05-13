using System;
using System.Collections.Generic;
using System.Text;

namespace OpenPdfAt
{
   public interface IPdfHandler
   {
      int OpenAtPage(int _page, string _filename);
      int OpenAtNamedDest(string _nameddest, string _filename);
   }
}
