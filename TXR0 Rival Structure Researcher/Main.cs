using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Memory;

namespace TXR0_Rival_Structure_Researcher
{
    public partial class Main : Form
    {
        private bool attached = false;
        private bool rivalDataLocated = false;
        private long rivalDataLocation = 0;
        private Mem MemLib = new Mem();

        public Main()
        {
            InitializeComponent();
        }

        private async Task<bool> attachTXR0Async()
        {
            if (attached || rivalDataLocated)
            {
                MemLib.CloseProcess();
                attached = false;
                rivalDataLocation = 0;
                rivalDataLocated = false;
                lblStatus.Text = "Detached";
                buttonAttach.Text = "Attach";
                disableUI();
                return false;
            }

            else
            {
                buttonAttach.Enabled = false;
                disableUI();
                if (MemLib.OpenProcess("pcsx2"))
                {
                    attached = true;
                    lblStatus.Text = "Attached - Searching for Rival Data...";
                }

                if(attached)
                {
                    var findLocation = await MemLib.AoBScan("base+0", (long)0x7fffffff,
                        "A0 55 26 00 00 00 00 00 C0 86 36 00 20 03 2D 00 " +
                        "00 00 00 00 D0 86 36 00 B0 56 26 00 00 00 00 00 " +
                        "E0 86 36 00 F0 56 26 00 00 00 00 00 F8 86 36 00 " +
                        "D0 56 26 00 02 00 00 00 00 00 00 00 00 00 00 00");

                    if (findLocation != 0)
                    {
                        rivalDataLocation = findLocation + 0x40;
                        lblStatus.Text = "Attached - Found Rival Data at 0x" + Convert.ToString(rivalDataLocation, 16);
                        buttonAttach.Text = "Detach";
                        rivalDataLocated = true;
                        buttonAttach.Enabled = true;
                        enableUI();
                        return true;
                    }
                    else
                    {
                        MemLib.CloseProcess();
                        attached = false;
                        lblStatus.Text = "Detached - Unable to find Rival Data. Is Game running?";
                        buttonAttach.Enabled = true;
                        return false;
                    }
                }

                MemLib.CloseProcess();
                attached = false;
                lblStatus.Text = "Detached - Unable to attach to PCSX2. Is it running?";
                buttonAttach.Enabled = true;
                return false;
            }
            return false;
        }

        private void readRivalData()
        {
            this.ActiveControl = null;
            if (rivalDataLocation != 0)
            {
                String rdBase = Convert.ToString((rivalDataLocation + (0x94 * Convert.ToInt32(RivalID.Value))), 16);

                value1.Text = MemLib.Read2Byte(rdBase + "+0").ToString();
                value2.Text = MemLib.Read2Byte(rdBase + "+2").ToString();
                value3.Text = MemLib.Read2Byte(rdBase + "+4").ToString();
                value4.Text = MemLib.Read2Byte(rdBase + "+6").ToString();
                value5.Text = MemLib.Read2Byte(rdBase + "+8").ToString();
                value6.Text = MemLib.Read2Byte(rdBase + "+A").ToString();
                value7.Text = MemLib.Read2Byte(rdBase + "+C").ToString();
                value8.Text = MemLib.Read2Byte(rdBase + "+E").ToString();
                value9.Text = MemLib.ReadInt(rdBase + "+10").ToString();
                value10.Text = MemLib.ReadString(rdBase + "+14", "", 0x10);
                value11.Text = MemLib.ReadString(rdBase + "+25", "", 0x10);
                value12.Text = MemLib.Read2Byte(rdBase + "+36").ToString();
                value13.Text = MemLib.Read2Byte(rdBase + "+38").ToString();
                value14.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+3A")).ToString();
                value15.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+3B")).ToString();
                value16.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+3C")).ToString();
                value17.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+3D")).ToString();
                value18.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+3E")).ToString();
                value19.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+3F")).ToString();
                value20.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+40")).ToString();
                value21.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+41")).ToString();
                value22.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+42")).ToString();
                value23.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+43")).ToString();
                value24.Text = MemLib.Read2Byte(rdBase + "+44").ToString();
                value25.Text = MemLib.ReadByte(rdBase + "+46").ToString();
                value26.Text = MemLib.ReadByte(rdBase + "+47").ToString();
                value27.Text = MemLib.ReadByte(rdBase + "+48").ToString();
                value28.Text = MemLib.ReadByte(rdBase + "+49").ToString();
                value29.Text = MemLib.ReadByte(rdBase + "+4A").ToString();
                value30.Text = MemLib.ReadByte(rdBase + "+4B").ToString();
                value31.Text = MemLib.ReadByte(rdBase + "+4C").ToString();
                value32.Text = MemLib.ReadByte(rdBase + "+4D").ToString();
                value33.Text = MemLib.ReadByte(rdBase + "+4E").ToString();
                value34.Text = MemLib.ReadByte(rdBase + "+4F").ToString();
                value35.Text = MemLib.ReadByte(rdBase + "+50").ToString();
                value36.Text = MemLib.ReadByte(rdBase + "+51").ToString();
                value37.Text = MemLib.ReadByte(rdBase + "+52").ToString();
                value38.Text = MemLib.ReadByte(rdBase + "+53").ToString();
                value39.Text = MemLib.ReadByte(rdBase + "+54").ToString();
                value40.Text = MemLib.ReadByte(rdBase + "+55").ToString();
                value41.Text = MemLib.ReadByte(rdBase + "+56").ToString();
                value42.Text = MemLib.ReadByte(rdBase + "+57").ToString();
                value43.Text = MemLib.ReadByte(rdBase + "+58").ToString();
                value44.Text = MemLib.ReadByte(rdBase + "+59").ToString();
                value45.Text = MemLib.ReadByte(rdBase + "+5A").ToString();
                value46.Text = MemLib.ReadByte(rdBase + "+5B").ToString();
                value47.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+5C")).ToString();
                value48.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+5D")).ToString();
                value49.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+5E")).ToString();
                value50.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+5F")).ToString();
                value51.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+60")).ToString();
                value52.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+61")).ToString();
                value53.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+62")).ToString();
                value54.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+63")).ToString();
                value55.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+64")).ToString();
                value56.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+65")).ToString();
                value57.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+66")).ToString();
                value58.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+67")).ToString();
                value59.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+68")).ToString();
                value60.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+69")).ToString();
                value61.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+6A")).ToString();
                value62.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+6B")).ToString();
                value63.Text = Convert.ToSByte((SByte)MemLib.ReadByte(rdBase + "+6C")).ToString();
                value64.Text = MemLib.ReadByte(rdBase + "+6D").ToString();
                value65.Text = MemLib.ReadByte(rdBase + "+6E").ToString();
                value66.Text = MemLib.ReadByte(rdBase + "+6F").ToString();

                float _R, _G, _B;
                _R = MemLib.ReadFloat(rdBase + "+70");
                _G = MemLib.ReadFloat(rdBase + "+74");
                _B = MemLib.ReadFloat(rdBase + "+78");

                Color color1;
                color1 = Color.FromArgb(Convert.ToInt32(_R * 255), Convert.ToInt32(_G * 255), Convert.ToInt32(_B * 255));
                buttonColor1.BackColor = color1;

                _R = MemLib.ReadFloat(rdBase + "+7C");
                _G = MemLib.ReadFloat(rdBase + "+80");
                _B = MemLib.ReadFloat(rdBase + "+84");

                Color color2;
                color2 = Color.FromArgb(Convert.ToInt32(_R * 255), Convert.ToInt32(_G * 255), Convert.ToInt32(_B * 255));
                buttonColor2.BackColor = color2;

                value69.Text = MemLib.ReadByte(rdBase + "+88").ToString();
                value70.Text = MemLib.ReadByte(rdBase + "+89").ToString();
                value71.Text = MemLib.ReadByte(rdBase + "+8A").ToString();
                value72.Text = MemLib.ReadByte(rdBase + "+8B").ToString();
                value73.Text = MemLib.ReadByte(rdBase + "+8C").ToString();
                value74.Text = MemLib.ReadByte(rdBase + "+8D").ToString();
                value75.Text = MemLib.ReadByte(rdBase + "+8E").ToString();
                value76.Text = MemLib.ReadByte(rdBase + "+8F").ToString();
                value77.Text = MemLib.ReadByte(rdBase + "+90").ToString();
                value78.Text = MemLib.ReadByte(rdBase + "+91").ToString();
                value79.Text = MemLib.ReadByte(rdBase + "+92").ToString();
                value80.Text = MemLib.ReadByte(rdBase + "+93").ToString();
            }
        }

        private void writeRivalData()
        {
            String rdBase = Convert.ToString((rivalDataLocation + (0x94 * Convert.ToInt32(RivalID.Value))), 16);

            MemLib.WriteBytes(rdBase + "+0", BitConverter.GetBytes(Convert.ToInt16(value1.Text)));
            MemLib.WriteBytes(rdBase + "+2", BitConverter.GetBytes(Convert.ToInt16(value2.Text)));
            MemLib.WriteBytes(rdBase + "+4", BitConverter.GetBytes(Convert.ToInt16(value3.Text)));
            MemLib.WriteBytes(rdBase + "+6", BitConverter.GetBytes(Convert.ToInt16(value4.Text)));
            MemLib.WriteBytes(rdBase + "+8", BitConverter.GetBytes(Convert.ToInt16(value5.Text)));
            MemLib.WriteBytes(rdBase + "+A", BitConverter.GetBytes(Convert.ToInt16(value6.Text)));
            MemLib.WriteBytes(rdBase + "+C", BitConverter.GetBytes(Convert.ToInt16(value7.Text)));
            MemLib.WriteBytes(rdBase + "+E", BitConverter.GetBytes(Convert.ToInt16(value8.Text)));
            MemLib.WriteBytes(rdBase + "+10", BitConverter.GetBytes(Convert.ToInt32(value9.Text)));
            MemLib.WriteBytes(rdBase + "+14", Encoding.ASCII.GetBytes(value10.Text));
            MemLib.WriteBytes(rdBase + "+25", Encoding.ASCII.GetBytes(value11.Text));
            MemLib.WriteBytes(rdBase + "+36", BitConverter.GetBytes(Convert.ToInt16(value12.Text)));
            MemLib.WriteBytes(rdBase + "+38", BitConverter.GetBytes(Convert.ToInt16(value13.Text)));
            MemLib.WriteBytes(rdBase + "+3A", BitConverter.GetBytes(Convert.ToSByte(value14.Text)));
            MemLib.WriteBytes(rdBase + "+3B", BitConverter.GetBytes(Convert.ToSByte(value15.Text)));
            MemLib.WriteBytes(rdBase + "+3C", BitConverter.GetBytes(Convert.ToSByte(value16.Text)));
            MemLib.WriteBytes(rdBase + "+3D", BitConverter.GetBytes(Convert.ToSByte(value17.Text)));
            MemLib.WriteBytes(rdBase + "+3E", BitConverter.GetBytes(Convert.ToSByte(value18.Text)));
            MemLib.WriteBytes(rdBase + "+3F", BitConverter.GetBytes(Convert.ToSByte(value19.Text)));
            MemLib.WriteBytes(rdBase + "+40", BitConverter.GetBytes(Convert.ToSByte(value20.Text)));
            MemLib.WriteBytes(rdBase + "+41", BitConverter.GetBytes(Convert.ToSByte(value21.Text)));
            MemLib.WriteBytes(rdBase + "+42", BitConverter.GetBytes(Convert.ToSByte(value22.Text)));
            MemLib.WriteBytes(rdBase + "+43", BitConverter.GetBytes(Convert.ToSByte(value23.Text)));
            MemLib.WriteBytes(rdBase + "+44", BitConverter.GetBytes(Convert.ToInt16(value24.Text)));
            MemLib.WriteBytes(rdBase + "+46", BitConverter.GetBytes(Convert.ToByte(value25.Text)));
            MemLib.WriteBytes(rdBase + "+47", BitConverter.GetBytes(Convert.ToByte(value26.Text)));
            MemLib.WriteBytes(rdBase + "+48", BitConverter.GetBytes(Convert.ToByte(value27.Text)));
            MemLib.WriteBytes(rdBase + "+49", BitConverter.GetBytes(Convert.ToByte(value28.Text)));
            MemLib.WriteBytes(rdBase + "+4A", BitConverter.GetBytes(Convert.ToByte(value29.Text)));
            MemLib.WriteBytes(rdBase + "+4B", BitConverter.GetBytes(Convert.ToByte(value30.Text)));
            MemLib.WriteBytes(rdBase + "+4C", BitConverter.GetBytes(Convert.ToByte(value31.Text)));
            MemLib.WriteBytes(rdBase + "+4D", BitConverter.GetBytes(Convert.ToByte(value32.Text)));
            MemLib.WriteBytes(rdBase + "+4E", BitConverter.GetBytes(Convert.ToByte(value33.Text)));
            MemLib.WriteBytes(rdBase + "+4F", BitConverter.GetBytes(Convert.ToByte(value34.Text)));
            MemLib.WriteBytes(rdBase + "+50", BitConverter.GetBytes(Convert.ToByte(value35.Text)));
            MemLib.WriteBytes(rdBase + "+51", BitConverter.GetBytes(Convert.ToByte(value36.Text)));
            MemLib.WriteBytes(rdBase + "+52", BitConverter.GetBytes(Convert.ToByte(value37.Text)));
            MemLib.WriteBytes(rdBase + "+53", BitConverter.GetBytes(Convert.ToByte(value38.Text)));
            MemLib.WriteBytes(rdBase + "+54", BitConverter.GetBytes(Convert.ToByte(value39.Text)));
            MemLib.WriteBytes(rdBase + "+55", BitConverter.GetBytes(Convert.ToByte(value40.Text)));
            MemLib.WriteBytes(rdBase + "+56", BitConverter.GetBytes(Convert.ToByte(value41.Text)));
            MemLib.WriteBytes(rdBase + "+57", BitConverter.GetBytes(Convert.ToByte(value42.Text)));
            MemLib.WriteBytes(rdBase + "+58", BitConverter.GetBytes(Convert.ToByte(value43.Text)));
            MemLib.WriteBytes(rdBase + "+59", BitConverter.GetBytes(Convert.ToByte(value44.Text)));
            MemLib.WriteBytes(rdBase + "+5A", BitConverter.GetBytes(Convert.ToByte(value45.Text)));
            MemLib.WriteBytes(rdBase + "+5B", BitConverter.GetBytes(Convert.ToByte(value46.Text)));
            MemLib.WriteBytes(rdBase + "+5C", BitConverter.GetBytes(Convert.ToSByte(value47.Text)));
            MemLib.WriteBytes(rdBase + "+5D", BitConverter.GetBytes(Convert.ToSByte(value48.Text)));
            MemLib.WriteBytes(rdBase + "+5E", BitConverter.GetBytes(Convert.ToSByte(value49.Text)));
            MemLib.WriteBytes(rdBase + "+5F", BitConverter.GetBytes(Convert.ToSByte(value50.Text)));
            MemLib.WriteBytes(rdBase + "+60", BitConverter.GetBytes(Convert.ToSByte(value51.Text)));
            MemLib.WriteBytes(rdBase + "+61", BitConverter.GetBytes(Convert.ToSByte(value52.Text)));
            MemLib.WriteBytes(rdBase + "+62", BitConverter.GetBytes(Convert.ToSByte(value53.Text)));
            MemLib.WriteBytes(rdBase + "+63", BitConverter.GetBytes(Convert.ToSByte(value54.Text)));
            MemLib.WriteBytes(rdBase + "+64", BitConverter.GetBytes(Convert.ToSByte(value55.Text)));
            MemLib.WriteBytes(rdBase + "+65", BitConverter.GetBytes(Convert.ToSByte(value56.Text)));
            MemLib.WriteBytes(rdBase + "+66", BitConverter.GetBytes(Convert.ToSByte(value57.Text)));
            MemLib.WriteBytes(rdBase + "+67", BitConverter.GetBytes(Convert.ToSByte(value58.Text)));
            MemLib.WriteBytes(rdBase + "+68", BitConverter.GetBytes(Convert.ToSByte(value59.Text)));
            MemLib.WriteBytes(rdBase + "+69", BitConverter.GetBytes(Convert.ToSByte(value60.Text)));
            MemLib.WriteBytes(rdBase + "+6A", BitConverter.GetBytes(Convert.ToSByte(value61.Text)));
            MemLib.WriteBytes(rdBase + "+6B", BitConverter.GetBytes(Convert.ToSByte(value62.Text)));
            MemLib.WriteBytes(rdBase + "+6C", BitConverter.GetBytes(Convert.ToSByte(value63.Text)));
            MemLib.WriteBytes(rdBase + "+6D", BitConverter.GetBytes(Convert.ToByte(value64.Text)));
            MemLib.WriteBytes(rdBase + "+6E", BitConverter.GetBytes(Convert.ToByte(value65.Text)));
            MemLib.WriteBytes(rdBase + "+6F", BitConverter.GetBytes(Convert.ToByte(value66.Text)));
            
            float _R, _G, _B;
            _R = (float)(buttonColor1.BackColor.R * 0.003921568627451);
            _G = (float)(buttonColor1.BackColor.G * 0.003921568627451);
            _B = (float)(buttonColor1.BackColor.B * 0.003921568627451);
            MemLib.WriteBytes(rdBase + "+70", BitConverter.GetBytes(_R));
            MemLib.WriteBytes(rdBase + "+74", BitConverter.GetBytes(_G));
            MemLib.WriteBytes(rdBase + "+78", BitConverter.GetBytes(_B));

            _R = (float)(buttonColor2.BackColor.R * 0.003921568627451);
            _G = (float)(buttonColor2.BackColor.G * 0.003921568627451);
            _B = (float)(buttonColor2.BackColor.B * 0.003921568627451);
            MemLib.WriteBytes(rdBase + "+7C", BitConverter.GetBytes(_R));
            MemLib.WriteBytes(rdBase + "+80", BitConverter.GetBytes(_G));
            MemLib.WriteBytes(rdBase + "+84", BitConverter.GetBytes(_B));

            MemLib.WriteBytes(rdBase + "+88", BitConverter.GetBytes(Convert.ToByte(value69.Text)));
            MemLib.WriteBytes(rdBase + "+89", BitConverter.GetBytes(Convert.ToByte(value70.Text)));
            MemLib.WriteBytes(rdBase + "+8A", BitConverter.GetBytes(Convert.ToByte(value71.Text)));
            MemLib.WriteBytes(rdBase + "+8B", BitConverter.GetBytes(Convert.ToByte(value72.Text)));
            MemLib.WriteBytes(rdBase + "+8C", BitConverter.GetBytes(Convert.ToByte(value73.Text)));
            MemLib.WriteBytes(rdBase + "+8D", BitConverter.GetBytes(Convert.ToByte(value74.Text)));
            MemLib.WriteBytes(rdBase + "+8E", BitConverter.GetBytes(Convert.ToByte(value75.Text)));
            MemLib.WriteBytes(rdBase + "+8F", BitConverter.GetBytes(Convert.ToByte(value76.Text)));
            MemLib.WriteBytes(rdBase + "+90", BitConverter.GetBytes(Convert.ToByte(value77.Text)));
            MemLib.WriteBytes(rdBase + "+91", BitConverter.GetBytes(Convert.ToByte(value78.Text)));
            MemLib.WriteBytes(rdBase + "+92", BitConverter.GetBytes(Convert.ToByte(value79.Text)));
            MemLib.WriteBytes(rdBase + "+93", BitConverter.GetBytes(Convert.ToByte(value80.Text)));
        }

        private void enableUI()
        {
            buttonRead.Enabled = true;
            buttonWrite.Enabled = true;

            value1.Enabled = true;
            value2.Enabled = true;
            value3.Enabled = true;
            value4.Enabled = true;
            value5.Enabled = true;
            value6.Enabled = true;
            value7.Enabled = true;
            value8.Enabled = true;
            value9.Enabled = true;
            value10.Enabled = true;
            value11.Enabled = true;
            value12.Enabled = true;
            value13.Enabled = true;
            value14.Enabled = true;
            value15.Enabled = true;
            value16.Enabled = true;
            value17.Enabled = true;
            value18.Enabled = true;
            value19.Enabled = true;
            value20.Enabled = true;
            value21.Enabled = true;
            value22.Enabled = true;
            value23.Enabled = true;
            value24.Enabled = true;
            value25.Enabled = true;
            value26.Enabled = true;
            value27.Enabled = true;
            value28.Enabled = true;
            value29.Enabled = true;
            value30.Enabled = true;
            value31.Enabled = true;
            value32.Enabled = true;
            value33.Enabled = true;
            value34.Enabled = true;
            value35.Enabled = true;
            value36.Enabled = true;
            value37.Enabled = true;
            value38.Enabled = true;
            value39.Enabled = true;
            value40.Enabled = true;
            value41.Enabled = true;
            value42.Enabled = true;
            value43.Enabled = true;
            value44.Enabled = true;
            value45.Enabled = true;
            value46.Enabled = true;
            value47.Enabled = true;
            value48.Enabled = true;
            value49.Enabled = true;
            value50.Enabled = true;
            value51.Enabled = true;
            value52.Enabled = true;
            value53.Enabled = true;
            value54.Enabled = true;
            value55.Enabled = true;
            value56.Enabled = true;
            value57.Enabled = true;
            value58.Enabled = true;
            value59.Enabled = true;
            value60.Enabled = true;
            value61.Enabled = true;
            value62.Enabled = true;
            value63.Enabled = true;
            value64.Enabled = true;
            value65.Enabled = true;
            value66.Enabled = true;
            buttonColor1.Enabled = true;
            buttonColor2.Enabled = true;
            value69.Enabled = true;
            value70.Enabled = true;
            value71.Enabled = true;
            value72.Enabled = true;
            value73.Enabled = true;
            value74.Enabled = true;
            value75.Enabled = true;
            value76.Enabled = true;
            value77.Enabled = true;
            value78.Enabled = true;
            value79.Enabled = true;
            value80.Enabled = true;
        }

        private void disableUI()
        {
            buttonRead.Enabled = false;
            buttonWrite.Enabled = false;
            value1.Enabled = false;
            value2.Enabled = false;
            value3.Enabled = false;
            value4.Enabled = false;
            value5.Enabled = false;
            value6.Enabled = false;
            value7.Enabled = false;
            value8.Enabled = false;
            value9.Enabled = false;
            value10.Enabled = false;
            value11.Enabled = false;
            value12.Enabled = false;
            value13.Enabled = false;
            value14.Enabled = false;
            value15.Enabled = false;
            value16.Enabled = false;
            value17.Enabled = false;
            value18.Enabled = false;
            value19.Enabled = false;
            value20.Enabled = false;
            value21.Enabled = false;
            value22.Enabled = false;
            value23.Enabled = false;
            value24.Enabled = false;
            value25.Enabled = false;
            value26.Enabled = false;
            value27.Enabled = false;
            value28.Enabled = false;
            value29.Enabled = false;
            value30.Enabled = false;
            value31.Enabled = false;
            value32.Enabled = false;
            value33.Enabled = false;
            value34.Enabled = false;
            value35.Enabled = false;
            value36.Enabled = false;
            value37.Enabled = false;
            value38.Enabled = false;
            value39.Enabled = false;
            value40.Enabled = false;
            value41.Enabled = false;
            value42.Enabled = false;
            value43.Enabled = false;
            value44.Enabled = false;
            value45.Enabled = false;
            value46.Enabled = false;
            value47.Enabled = false;
            value48.Enabled = false;
            value49.Enabled = false;
            value50.Enabled = false;
            value51.Enabled = false;
            value52.Enabled = false;
            value53.Enabled = false;
            value54.Enabled = false;
            value55.Enabled = false;
            value56.Enabled = false;
            value57.Enabled = false;
            value58.Enabled = false;
            value59.Enabled = false;
            value60.Enabled = false;
            value61.Enabled = false;
            value62.Enabled = false;
            value63.Enabled = false;
            value64.Enabled = false;
            value65.Enabled = false;
            value66.Enabled = false;
            buttonColor1.Enabled = false;
            buttonColor2.Enabled = false;
            value69.Enabled = false;
            value70.Enabled = false;
            value71.Enabled = false;
            value72.Enabled = false;
            value73.Enabled = false;
            value74.Enabled = false;
            value75.Enabled = false;
            value76.Enabled = false;
            value77.Enabled = false;
            value78.Enabled = false;
            value79.Enabled = false;
            value80.Enabled = false;
        }

        private static void UpdateAppSettings(String key, String value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("Error writing app settings");
            }
        }

        private async void buttonAttach_Click_1(object sender, EventArgs e)
        {
            var attachToTXR0 = await attachTXR0Async();
            if(attachToTXR0 == true)
            { 
                readRivalData();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            disableUI();
            label1.Text = ConfigurationManager.AppSettings["value1"];
            label2.Text = ConfigurationManager.AppSettings["value2"];
            label3.Text = ConfigurationManager.AppSettings["value3"];
            label4.Text = ConfigurationManager.AppSettings["value4"];
            label5.Text = ConfigurationManager.AppSettings["value5"];
            label6.Text = ConfigurationManager.AppSettings["value6"];
            label7.Text = ConfigurationManager.AppSettings["value7"];
            label8.Text = ConfigurationManager.AppSettings["value8"];
            label9.Text = ConfigurationManager.AppSettings["value9"];
            label10.Text = ConfigurationManager.AppSettings["value10"];
            label11.Text = ConfigurationManager.AppSettings["value11"];
            label12.Text = ConfigurationManager.AppSettings["value12"];
            label13.Text = ConfigurationManager.AppSettings["value13"];
            label14.Text = ConfigurationManager.AppSettings["value14"];
            label15.Text = ConfigurationManager.AppSettings["value15"];
            label16.Text = ConfigurationManager.AppSettings["value16"];
            label17.Text = ConfigurationManager.AppSettings["value17"];
            label18.Text = ConfigurationManager.AppSettings["value18"];
            label19.Text = ConfigurationManager.AppSettings["value19"];
            label20.Text = ConfigurationManager.AppSettings["value20"];
            label21.Text = ConfigurationManager.AppSettings["value21"];
            label22.Text = ConfigurationManager.AppSettings["value22"];
            label23.Text = ConfigurationManager.AppSettings["value23"];
            label24.Text = ConfigurationManager.AppSettings["value24"];
            label25.Text = ConfigurationManager.AppSettings["value25"];
            label26.Text = ConfigurationManager.AppSettings["value26"];
            label27.Text = ConfigurationManager.AppSettings["value27"];
            label28.Text = ConfigurationManager.AppSettings["value28"];
            label29.Text = ConfigurationManager.AppSettings["value29"];
            label30.Text = ConfigurationManager.AppSettings["value30"];
            label31.Text = ConfigurationManager.AppSettings["value31"];
            label32.Text = ConfigurationManager.AppSettings["value32"];
            label33.Text = ConfigurationManager.AppSettings["value33"];
            label34.Text = ConfigurationManager.AppSettings["value34"];
            label35.Text = ConfigurationManager.AppSettings["value35"];
            label36.Text = ConfigurationManager.AppSettings["value36"];
            label37.Text = ConfigurationManager.AppSettings["value37"];
            label38.Text = ConfigurationManager.AppSettings["value38"];
            label39.Text = ConfigurationManager.AppSettings["value39"];
            label40.Text = ConfigurationManager.AppSettings["value40"];
            label41.Text = ConfigurationManager.AppSettings["value41"];
            label42.Text = ConfigurationManager.AppSettings["value42"];
            label43.Text = ConfigurationManager.AppSettings["value43"];
            label44.Text = ConfigurationManager.AppSettings["value44"];
            label45.Text = ConfigurationManager.AppSettings["value45"];
            label46.Text = ConfigurationManager.AppSettings["value46"];
            label47.Text = ConfigurationManager.AppSettings["value47"];
            label48.Text = ConfigurationManager.AppSettings["value48"];
            label49.Text = ConfigurationManager.AppSettings["value49"];
            label50.Text = ConfigurationManager.AppSettings["value50"];
            label51.Text = ConfigurationManager.AppSettings["value51"];
            label52.Text = ConfigurationManager.AppSettings["value52"];
            label53.Text = ConfigurationManager.AppSettings["value53"];
            label54.Text = ConfigurationManager.AppSettings["value54"];
            label55.Text = ConfigurationManager.AppSettings["value55"];
            label56.Text = ConfigurationManager.AppSettings["value56"];
            label57.Text = ConfigurationManager.AppSettings["value57"];
            label58.Text = ConfigurationManager.AppSettings["value58"];
            label59.Text = ConfigurationManager.AppSettings["value59"];
            label60.Text = ConfigurationManager.AppSettings["value60"];
            label61.Text = ConfigurationManager.AppSettings["value61"];
            label62.Text = ConfigurationManager.AppSettings["value62"];
            label63.Text = ConfigurationManager.AppSettings["value63"];
            label64.Text = ConfigurationManager.AppSettings["value64"];
            label65.Text = ConfigurationManager.AppSettings["value65"];
            label66.Text = ConfigurationManager.AppSettings["value66"];
            label67.Text = ConfigurationManager.AppSettings["value67"];
            label68.Text = ConfigurationManager.AppSettings["value68"];
            label69.Text = ConfigurationManager.AppSettings["value69"];
            label70.Text = ConfigurationManager.AppSettings["value70"];
            label71.Text = ConfigurationManager.AppSettings["value71"];
            label72.Text = ConfigurationManager.AppSettings["value72"];
            label73.Text = ConfigurationManager.AppSettings["value73"];
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            readRivalData();
        }

        private void buttonWrite_Click(object sender, EventArgs e)
        {
            writeRivalData();
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Add:
                    RivalID.UpButton();
                    break;
                case Keys.Subtract:
                    RivalID.DownButton();
                    break;
                case Keys.F5:
                    readRivalData();
                    break;
                case Keys.F6:
                    writeRivalData();
                    break;
                default:
                    break;
            }
            this.ActiveControl = null;
        }

        private void labelDoubleClick(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                Label workingLabel = (Label)sender;
                LabelEdit labelEdit = new LabelEdit(workingLabel.Text);
                labelEdit.ShowDialog();

                if (labelEdit.DialogResult == DialogResult.OK)
                {
                    workingLabel.Text = labelEdit.getName();

                    String appKey = "value" + ((Label)sender).Name.ToString().Trim(new char[] { 'l', 'a', 'b', 'e' });
                    UpdateAppSettings(appKey, workingLabel.Text);
                }
            }
        }

        private void buttonColor1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = buttonColor1.BackColor;

            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                buttonColor1.BackColor = colorDialog.Color;
            }
        }

        private void buttonColor2_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = buttonColor2.BackColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                buttonColor2.BackColor = colorDialog.Color;
            }
        }
    }
}
