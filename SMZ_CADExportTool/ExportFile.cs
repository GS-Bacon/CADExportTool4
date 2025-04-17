using SolidworksAPIAPI;
using SolidworksAPIAPI.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMZ_CADExportTool
{
    public class ExportFile
    {
        private IExportOption ExportOption;
        private CADExportTool exportTool;
        public ExportFile(CADExportTool form)
        {
            this.exportTool = form;
        }
        public void Export()
        {
            SolidworksAPIAPI.ExportOption exportOption = new SolidworksAPIAPI.ExportOption();

            exportOption.FilePaths = new List<string>();
            foreach (ListViewItem item in exportTool.FilePath_ListView.Items)
            {
                exportOption.FilePaths.Add(item.SubItems[1].Text);
            }

            //出力先のフォルダーを取得する
            //トップフォルダーを取得
            string topFolderPath = Path.GetDirectoryName(exportTool.FilePath_ListView.Items[0].SubItems[1].Text);

            //オプションによって下位フォルダーを選択する
            if (exportTool.SameFolder_RadioButton.Checked)
            {

            }
            else if (exportTool.LowerFolder_RadioButton.Checked)
            {
                //直下フォルダー
                topFolderPath = exportTool.LowerFolder_ComboBox.Items[0].ToString();
            }
            else if (exportTool.OtherFolder_RadioButton.Checked)
            {
                //他のフォルダー
                topFolderPath = exportTool.ZipOtherFolder_ListBox.Items[0].ToString();
            }

            //各拡張子ごとのフォルダーを選択
            if (exportTool.PDF_CheckBox.Checked)
            {
                exportOption.Extensions.Add(new ExtensionPair(".pdf", Path.Combine(topFolderPath,"PDF")));
            }
            if (exportTool.DXF_CheckBox.Checked)
            {
                exportOption.Extensions.Add(new ExtensionPair(".dxf", Path.Combine(topFolderPath, "dxf")));
            }
            if (exportTool.IGS_CheckBox.Checked)
            {
                exportOption.Extensions.Add(new ExtensionPair(".igs", Path.Combine(topFolderPath, "IGS")));
            }
            if (exportTool.STEP_CheckBox.Checked)
            {
                exportOption.Extensions.Add(new ExtensionPair(".step", Path.Combine(topFolderPath, "STEP")));
            }
            if(exportTool.ThreeMF_CheckBox.Checked)
            {
                exportOption.Extensions.Add(new ExtensionPair(".3mf", Path.Combine(topFolderPath, "3MF")));
            }
            GenerateConverter generateConverter = new GenerateConverter();
            List<IConverter> converters = generateConverter.Generate(this.ExportOption);

            List<string>? result = new();

            foreach (IConverter converter in converters)
            {
                result.AddRange(converter.Convert());
            }

        }

    }
}
