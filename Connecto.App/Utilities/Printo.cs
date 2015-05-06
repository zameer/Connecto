using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;

namespace Connecto.App.Utilities
{
    public static class Printo
    {
        static Warning[] _warnings;
        private static int _mCurrentPageIndex;
        private static IList<Stream> _mStreams;
        public static Tuple<byte[], string> File(LocalReport report, string deviceInfo, string reportType)
        {
            string mimeType;
            string encoding;
            string fileNameExtension;

            string[] streams;
            var renderedBytes = report.Render(reportType, deviceInfo, out mimeType, out encoding,
                out fileNameExtension,
                out streams,
                out _warnings);
            return new Tuple<byte[], string>(renderedBytes, mimeType);
        }

        // Export the given report as an EMF (Enhanced Metafile) file.
        public static void Printer(LocalReport report, string deviceInfo)
        {
            _mStreams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out _warnings);
            foreach (var stream in _mStreams)
                stream.Position = 0;
            Print();
        }
        private static Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            var stream = new MemoryStream();
            _mStreams.Add(stream);
            return stream;
        }
        // Handler for PrintPageEvents
        private static void PrintPage(object sender, PrintPageEventArgs ev)
        {
            var pageImage = new Metafile(_mStreams[_mCurrentPageIndex]);

            // Adjust rectangular area with printer margins.
            var adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            _mCurrentPageIndex++;
            ev.HasMorePages = (_mCurrentPageIndex < _mStreams.Count);
        }
        private static void Print()
        {
            if (_mStreams == null || _mStreams.Count == 0) throw new Exception("Error: no stream to print.");
            var printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
                throw new Exception("Error: cannot find the default printer.");

            printDoc.PrintPage += PrintPage;
            _mCurrentPageIndex = 0;
            printDoc.Print();
        }
    }
}