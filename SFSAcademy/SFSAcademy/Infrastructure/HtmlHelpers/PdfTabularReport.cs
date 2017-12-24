using System;
using System.Linq;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Collections.Generic;

namespace SFSAcademy.HtmlHelpers
{
    // The main class used to generate Pdf tabular report.
    public class PdfTabularReport
    {
        // Configurations
        public ReportConfiguration ReportConfiguration { get; set; }

        // Internal properties, not set outside of the class
        public Image LogoImage { get; private set; }
        public PdfPTable Title { get; private set; }
        public PdfPTable PageNumberLabel { get; private set; }
        public float HeaderSectionHeight { get; private set; }
        public int PageCount { get; private set; }

        // Private instance variables
        Document PdfDocument = null;
        PdfWriter PdfWriter = null;
        MemoryStream PdfStream = null;

        // Constructor
        public PdfTabularReport(ReportConfiguration configuration = null)
        {
            // If configuration is not provided, the default will be used
            ReportConfiguration = configuration ?? new ReportConfiguration();
        }

        private void InitiateDocument()
        {
            PdfStream = new MemoryStream();
            PdfDocument = new Document(ReportConfiguration.PageOrientation);
            PdfDocument.SetMargins(ReportConfiguration.MarginLeft,
                ReportConfiguration.MarginRight,
                ReportConfiguration.MarginTop, ReportConfiguration.MarginBottom);
            PdfWriter = PdfWriter.GetInstance(PdfDocument, PdfStream);

            // create logo, header and page number objects
            PdfPCell cell;
            HeaderSectionHeight = 0;
            LogoImage = null;
            if (ReportConfiguration.LogoPath != null)
            {
                LogoImage = Image.GetInstance(ReportConfiguration.LogoPath);
                LogoImage.ScalePercent(ReportConfiguration.LogImageScalePercent);
                LogoImage.SetAbsolutePosition(PdfDocument.LeftMargin,
                    PdfDocument.PageSize.Height - PdfDocument.TopMargin
                        - LogoImage.ScaledHeight);

                HeaderSectionHeight = LogoImage.ScaledHeight;
            }

            Title = null;
            float titleHeight = 0;
            if ((ReportConfiguration.ReportTitle != null) ||
                (ReportConfiguration.ReportSubTitle != null))
            {
                Title = new PdfPTable(1);
                Title.TotalWidth = PdfDocument.PageSize.Width
                    - (PdfDocument.LeftMargin + PdfDocument.RightMargin);

                if (ReportConfiguration.ReportTitle != null)
                {
                    cell = new PdfPCell(new Phrase(ReportConfiguration.ReportTitle,
                                               new Font(ReportFonts.HelveticaBold, 12)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Border = 0;
                    Title.AddCell(cell);
                }

                if (ReportConfiguration.ReportSubTitle != null)
                {
                    cell = new PdfPCell(new Phrase(ReportConfiguration.ReportSubTitle,
                                               new Font(ReportFonts.Helvetica, 10)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.PaddingTop = 5;
                    cell.Border = 0;
                    Title.AddCell(cell);
                }

                // Get the height of the title section
                for (int i = 0; i < Title.Rows.Count; i++)
                {
                    titleHeight = titleHeight + Title.GetRowHeight(i);
                }
            }
            HeaderSectionHeight = (HeaderSectionHeight > titleHeight)
                ? HeaderSectionHeight : titleHeight;


            PageNumberLabel = new PdfPTable(1);
            PageNumberLabel.TotalWidth = PdfDocument.PageSize.Width
                               - (PdfDocument.LeftMargin + PdfDocument.RightMargin);
            cell = new PdfPCell(new Phrase
        ("Page Label", new Font(ReportFonts.Helvetica, 8)));
            cell.Border = 0;
            float pagenumberHeight = PageNumberLabel.GetRowHeight(0);
            HeaderSectionHeight = (HeaderSectionHeight > pagenumberHeight)
                ? HeaderSectionHeight : pagenumberHeight;
        }

        private MemoryStream RenderDocument(ReportTable reportTable)
        {
            PdfWriter.PageEvent = new PageEventHelper { Report = this };
            PdfDocument.Open();
            reportTable.RenderTable(PdfDocument, PdfWriter);
            PdfDocument.Close();
            PdfWriter.Flush();
            return PdfStream;
        }

        // Method to get the pdf stream
        public MemoryStream GetPdf<T>(List<T> data, List<ReportColumn> displayColumns)
        {
            InitiateDocument();

            // Add the report data
            var top = (HeaderSectionHeight == 0)
                          ? PdfDocument.PageSize.Height - PdfDocument.TopMargin
                          : PdfDocument.PageSize.Height - PdfDocument.TopMargin
                          - HeaderSectionHeight - 10;
            var reportTable = ReportTable.CreateReportTable<T>(data,
                displayColumns, top, PdfDocument);
            PageCount = reportTable.PageCount;

            return RenderDocument(reportTable);
        }

        // Overloaded method to get the pdf stream. It takes that data as DataTable
        public MemoryStream GetPdf(DataTable data, List<ReportColumn> displayColumns)
        {
            List<DataRow> list = data.AsEnumerable().ToList();
            return GetPdf<DataRow>(list, displayColumns);
        }
    }

    // Fonts used by the tabular report
    public class ReportFonts
    {
        public static BaseFont Helvetica
        {
            get
            {
                return BaseFont.CreateFont(BaseFont.HELVETICA,
              BaseFont.CP1252, false);
            }
        }
        public static BaseFont HelveticaBold
        {
            get
            {
                return BaseFont.CreateFont(BaseFont.HELVETICA_BOLD,
              BaseFont.CP1252, false);
            }
        }
    }

    // Utility class to render the Pdf table to the report document
    internal class ReportTable
    {
        private PdfPTable headerTable;
        private PdfPTable dataTable;
        private List<Tuple<int, int>> pageSplitter;
        private float width;
        private float top;
        private float height;

        public int PageCount
        {
            get { return (pageSplitter.Count == 0) ? 1 : pageSplitter.Count; }
        }

        // Private constructor. The instances need to use "CreateReportTable"
        // method to create.
        private ReportTable(List<ReportColumn> displayColumns,
            Document document, float top)
        {
            pageSplitter = new List<Tuple<int, int>>();
            this.top = top;
            width = document.PageSize.Width
                              - document.LeftMargin - document.RightMargin;
            height = top - document.BottomMargin;

            float[] columnWidths =
                (from c in displayColumns select (float)c.Width).ToArray();
            headerTable = new PdfPTable(columnWidths);
            headerTable.TotalWidth = width;
            dataTable = new PdfPTable(columnWidths);
            dataTable.TotalWidth = width;

            foreach (var column in displayColumns)
            {
                AddCell(headerTable, column.HeaderText,
                    new Font(ReportFonts.HelveticaBold, 10), BaseColor.WHITE, 5f);
            }
        }

        private static void AddCell(PdfPTable table, string Text, Font font,
            BaseColor backgroundColor = null, float padding = 3f)
        {
            PdfPCell cell = new PdfPCell(new Paragraph(Text, font));
            cell.Padding = padding;
            cell.Border = 0;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.BackgroundColor = backgroundColor ?? BaseColor.WHITE;
            table.AddCell(cell);
        }

        private static void AddRow(Object dataitem, System.Type type,
            List<ReportColumn> displayColumns, BaseColor color, PdfPTable table)
        {
            foreach (var column in displayColumns)
            {
                var text = string.Empty;

                if (type.FullName == "System.Data.DataRow")
                {
                    text = ((DataRow)dataitem)[column.ColumnName].ToString();
                }
                else
                {
                    var propertyInfo = type.GetProperty(column.ColumnName);
                    text = (propertyInfo.GetValue(dataitem, null) == null)
                               ? ""
                               : propertyInfo.GetValue(dataitem, null).ToString();
                }

                AddCell(table, text, new Font(ReportFonts.Helvetica, 8), color);
            }
        }

        // Static method to create & return an instance object
        public static ReportTable CreateReportTable<T>(List<T> data,
            List<ReportColumn> displayColumns, float top, Document document)
        {
            // Construct an instance object
            var reportTable = new ReportTable(displayColumns, document, top);
            var type = typeof(T);

            // Add each data item into the PdfPTable.
            int srartRow = 0;
            int pageRowIndex = 0;
            float headerHeight = reportTable.headerTable.GetRowHeight(0);
            float actualHeight = headerHeight;
            for (int i = 0; i < data.Count; i++)
            {
                var dataItem = data[i];
                BaseColor color = (pageRowIndex++ % 2 == 0)
                    ? BaseColor.LIGHT_GRAY : BaseColor.WHITE;

                AddRow(dataItem, type, displayColumns, color, reportTable.dataTable);

                actualHeight = actualHeight + reportTable.dataTable.GetRowHeight(i);
                var lastRowReached = i == data.Count - 1;
                if ((actualHeight > reportTable.height) || lastRowReached)
                {
                    reportTable.pageSplitter.Add(new Tuple<int, int>(srartRow,
                        lastRowReached ? -1 : i));

                    if (!lastRowReached)
                    {
                        reportTable.dataTable.DeleteLastRow();
                        AddRow(dataItem, type, displayColumns, BaseColor.LIGHT_GRAY,
                            reportTable.dataTable);
                        pageRowIndex = 1;
                    }

                    actualHeight = headerHeight + reportTable.dataTable.GetRowHeight(i);
                    srartRow = i;
                }
            }

            return reportTable;
        }

        // Render the table to the Pdf document.
        public void RenderTable(Document document, PdfWriter writer)
        {
            float left = (document.PageSize.Width - headerTable.TotalWidth) / 2;

            var pageCount = pageSplitter.Count;
            float headerHeight = headerTable.GetRowHeight(0);
            for (int i = 0; i < pageCount; i++)
            {
                var rownumbers = pageSplitter[i];
                headerTable.WriteSelectedRows(0, 1, left, top, writer.DirectContent);
                dataTable.WriteSelectedRows(rownumbers.Item1, rownumbers.Item2,
                        left, top - headerHeight, writer.DirectContent);

                if (i != pageCount - 1)
                {
                    document.NewPage();
                }
            }
        }
    }

    // PdfPageEventHelper: logo, title, sub-title, and page numbers.
    public class PageEventHelper : PdfPageEventHelper
    {
        public PdfTabularReport Report { get; set; }

        private void AddHeader(Document document, PdfWriter writer)
        {
            // Add logo
            if (Report.LogoImage != null)
            {
                document.Add(Report.LogoImage);
            }

            // Add titles
            if (Report.Title != null)
            {
                Report.Title.WriteSelectedRows(0, -1, document.LeftMargin,
                    document.PageSize.Height - document.TopMargin, writer.DirectContent);
            }

            // Add page number
            Report.PageNumberLabel.DeleteLastRow();
            var cell = new PdfPCell(new Phrase("Page " + document.PageNumber.ToString()
                + " of "
                + Report.PageCount.ToString(), new Font(ReportFonts.Helvetica, 8)));
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Report.PageNumberLabel.AddCell(cell);
            var cellHeight = Report.PageNumberLabel.GetRowHeight(0);
            Report.PageNumberLabel.WriteSelectedRows(0, -1, document.LeftMargin,
                document.PageSize.Height - document.TopMargin - Report.HeaderSectionHeight
                + cellHeight, writer.DirectContent);
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
            AddHeader(document, writer);
        }
    }
}