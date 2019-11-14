using IronPdf;
using LanguageCenterPLC.Application.ViewModels.Report;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LanguageCenterPLC.Application.ReportGenerate
{
    public class ReportControl
    {
        public static string GetHTMLString(List<ReportViewModel> reports)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            var sb = new StringBuilder();
            sb.AppendFormat(
                @"<!DOCTYPE html>
                <html lang='en'>

                    <head>
  <title>Table V01</title>
  <meta charset='UTF-8'>
  <meta name='viewport' content='width=device-width, initial-scale=1'>
  <link href='~/../custom-report.css' rel='stylesheet' />
  <meta charset='utf-8'>
  <meta name='viewport' content='width=device-width, initial-scale=1'>
  <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css'>
  <script src='https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js'></script>
  <script src='https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js'></script>
  <script src='https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js'></script>
 <style type='text/css'>
    @media print {
      .page -break {
                height: 0;
                    page -break-before: always;
                margin: 0;
                    border - top: none;
                }
            }

            body,
    p,
    a,
    span,
    td {
                font - size: 9pt;
                font - family: Arial, Helvetica, sans - serif;
            }

            body {
                margin - left: 2em;
                margin - right: 2em;
            }

    .page {
            height: 947px;
                padding - top: 5px;
                page -break-after: always;
                font - family: Arial, Helvetica, sans - serif;
            position: relative;
                border - bottom: 1px solid #000;
    }
  </ style >
</head>

<body>

 
    <div style='width: 100%; float: left;'>
      <div style='width: 50%;float: left;'>
        <div style='width: 100%;float: left; text-align: center '>
          Trung Tâm Ngoại Ngữ PLC
        </div>
        <div style='width: 100%;float: left;  text-align: center'>
          ĐC: 97/7C - Lê Hồng Phong - Ngô Quyền - Hải Phòng
        </div>
      </div>
      <div style='width: 50%;float: left; text-align: center;'>
        Ngày in:07/08/201910:25:46SA
      </div>
    </div>








    <div style=' width: 100%; text-align: center;'>
      <h1>DANH SÁCH LỚP</h1>
    </div>

    <div class='row'>
      <div class='col-12'>
        <table class='table'>
          <thead class='thead-dark'>
            <tr>
              <th scope='col'>STT</th>
              <th scope='col'>Họ và tên</th>
              <th scope='col'>Năm sinh</th>
              <th scope='col'>GT</th>
              <th scope='col'>Điện thoại</th>
              <th scope='col'>Lớp</th>
              <th scope='col'>Ghi chú</th>
            </tr>
          </thead>
          <tbody>");

            int i = 1;
            foreach (var report in reports)
            {
                sb.AppendFormat(
                    @"<tr>
                        <th scope='row'>{0}</th>
                        <td>{1}</td>
                        <td>{2}</td>
                        <td>{3}</td>
                        <td>{4}</td>
                        <td>{5}</td>
                        <td>{6}</td>
                    </tr>", report.Index, report.FullName, report.YearOfBirth,
                    report.Gender, report.Phone, report.ClassName,report.Note);
                i++;
            }

            sb.AppendFormat(
                @" </tbody>
                    </table>

                    </div>

                    <div style='width: 100%; float: right;'>

                    <div style='width: 30%;float: right; text-align: center;'><b>Người lập phiếu</b></div>
                    </div>
                    </div>
           
                    </body>

                </html>
                "
                );
            return sb.ToString();
        }


        public static PdfDocument RenderPDF(string Html)
        {
            var Renderer = new IronPdf.HtmlToPdf();
            Renderer.PrintOptions.InputEncoding = Encoding.UTF8;
            Renderer.PrintOptions.EnableJavaScript = true;
            Renderer.PrintOptions.RenderDelay = 500;
            Renderer.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.Note;
            Renderer.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Portrait;
            PdfDocument PDF = Renderer.RenderHtmlAsPdf(Html);
            PDF.WatermarkAllPages(" < h2 style='color:red'>SAMPLE</h2>", PdfDocument.WaterMarkLocation.MiddleCenter, 50, -45, "https://www.nuget.org/packages/IronPdf");
            return PDF;
        }
    }
}
