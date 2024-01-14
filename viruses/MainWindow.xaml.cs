using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using VirusGenerator;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace viruses
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        private List<Symptom> symptomsList = new List<Symptom>();
        private List<List<TextBox>> textBoxList = new List<List<TextBox>>();
        private List<ComboBox> comboBoxList = new List<ComboBox>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeViruses();
            InitializeTextBoxList();
            InitializeComboBoxList();
            LoadComboBox();
            InitializeComboBoxesDropDown();
        }

        private void InitializeTextBoxList()
        {
            List<TextBox> list = new List<TextBox>();
            list.Add(TextBox11);
            list.Add(TextBox12);
            list.Add(TextBox13);
            list.Add(TextBox14);
            list.Add(TextBox15);
            textBoxList.Add(new List<TextBox>(list));
            list.Clear();
            list.Add(TextBox21);
            list.Add(TextBox22);
            list.Add(TextBox23);
            list.Add(TextBox24);
            list.Add(TextBox25);
            textBoxList.Add(new List<TextBox>(list));
            list.Clear();
            list.Add(TextBox31);
            list.Add(TextBox32);
            list.Add(TextBox33);
            list.Add(TextBox34);
            list.Add(TextBox35);
            textBoxList.Add(new List<TextBox>(list));
            list.Clear();
            list.Add(TextBox41);
            list.Add(TextBox42);
            list.Add(TextBox43);
            list.Add(TextBox44);
            list.Add(TextBox45);
            textBoxList.Add(new List<TextBox>(list));
            list.Clear();
            list.Add(TextBox51);
            list.Add(TextBox52);
            list.Add(TextBox53);
            list.Add(TextBox54);
            list.Add(TextBox55);
            textBoxList.Add(new List<TextBox>(list));
            list.Clear();
            list.Add(TextBox61);
            list.Add(TextBox62);
            list.Add(TextBox63);
            list.Add(TextBox64);
            list.Add(TextBox65);
            textBoxList.Add(new List<TextBox>(list));
        }

        private void InitializeComboBoxList()
        {
            comboBoxList.Add(ComboBox1);
            comboBoxList.Add(ComboBox2);
            comboBoxList.Add(ComboBox3);
            comboBoxList.Add(ComboBox4);
            comboBoxList.Add(ComboBox5);
        }
        private void InitializeComboBoxesDropDown()
        {
            ComboBox1.DropDownOpened += new EventHandler(ComboBox1_DropDownOpened);
            ComboBox1.DropDownClosed += new EventHandler(ComboBox1_DropDownClosed);

            ComboBox2.DropDownOpened += new EventHandler(ComboBox2_DropDownOpened);
            ComboBox2.DropDownClosed += new EventHandler(ComboBox2_DropDownClosed);

            ComboBox3.DropDownOpened += new EventHandler(ComboBox3_DropDownOpened);
            ComboBox3.DropDownClosed += new EventHandler(ComboBox3_DropDownClosed);

            ComboBox4.DropDownOpened += new EventHandler(ComboBox4_DropDownOpened);
            ComboBox4.DropDownClosed += new EventHandler(ComboBox4_DropDownClosed);

            ComboBox5.DropDownOpened += new EventHandler(ComboBox5_DropDownOpened);
            ComboBox5.DropDownClosed += new EventHandler(ComboBox5_DropDownClosed);

            ComboBox6.DropDownOpened += new EventHandler(ComboBox6_DropDownOpened);
            ComboBox6.DropDownClosed += new EventHandler(ComboBox6_DropDownClosed);
        }

        private void InitializeViruses()
        {
            GetSymptoms();
            foreach (string line in File.ReadLines(Environment.CurrentDirectory + "/viruses.txt"))
            {
                symptomsList.Add(new Symptom( line.Split("\t")));                
            }            
        }

        private void LoadComboBox()
        {
            var listSpan = CollectionsMarshal.AsSpan(comboBoxList);
            foreach (ref ComboBox comboBox in listSpan)
            {
                comboBox.Items.Clear();
                foreach (Symptom symptom in symptomsList)
                {
                    if (!symptom.isActive)
                    {
                        comboBox.Items.Add(symptom.name);
                    }
                }
                comboBox.SelectedIndex = -1;
            }
        }

        private void virusesListActiveUpdate(ref ComboBox comboBox, int index)
        {
            if (comboBox.SelectedIndex != -1)
            {               
                foreach (Symptom symptom in symptomsList)
                {
                    if (symptom.name == comboBox.SelectedItem.ToString())
                    {
                        symptom.changeState();
                        if (symptom.isActive)
                        {
                            symptom.selectedIndex = index;
                        }
                        else
                        {
                            symptom.selectedIndex = -1;
                        }                        
                        break;
                    }
                }
            }
            textBoxesUpdate();
        }

        private void zeroUpdate(ref ComboBox comboBox)
        {
            foreach (Symptom symptom in symptomsList)
            {
                if (symptom.name == comboBox.SelectedValue.ToString())
                {
                    symptom.changeState();
                    symptom.selectedIndex = -1;
                }
            }
            comboBox.SelectedIndex = -1;
            textBoxesUpdate();
        }

        private string getParam(int index, int param)
        {
            foreach (Symptom symptom in symptomsList)
            {
                if ( symptom.selectedIndex == index)
                {
                    return symptom.getParam(param);
                }
            }
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
            return null;
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
        }

        private void ClearTextBoxes(int index)
        {
            foreach (TextBox textBox in textBoxList[index])
            {
                textBox.Text = "";
            }
        }

        private void textBoxesUpdate()
        {
            if (ComboBox1.SelectedIndex != -1)
            {
                int count = 1;
                foreach (TextBox textBox in textBoxList[0])
                {
                    textBox.Text = getParam(1, count++);
                }
            }
            else
            {
                ClearTextBoxes(0);
            }
            if (ComboBox2.SelectedIndex != -1)
            {
                int count = 1;
                foreach (TextBox textBox in textBoxList[1])
                {
                    textBox.Text = getParam(2, count++);
                }
            }
            else
            {
                ClearTextBoxes(1);
            }
            if (ComboBox3.SelectedIndex != -1)
            {
                int count = 1;
                foreach (TextBox textBox in textBoxList[2])
                {
                    textBox.Text = getParam(3, count++);
                }
            }
            else
            {
                ClearTextBoxes(2);
            }
            if (ComboBox4.SelectedIndex != -1)
            {
                int count = 1;
                foreach (TextBox textBox in textBoxList[3])
                {
                    textBox.Text = getParam(4, count++);
                }
            }
            else
            {
                ClearTextBoxes(3);
            }
            if (ComboBox5.SelectedIndex != -1)
            {
                int count = 1;
                foreach (TextBox textBox in textBoxList[4])
                {
                    textBox.Text = getParam(5, count++);
                }
            }
            else
            {
                ClearTextBoxes(4);
            }
            if (ComboBox6.SelectedIndex != -1)
            {
                int count = 1;
                foreach (TextBox textBox in textBoxList[5])
                {
                    textBox.Text = getParam(6, count++);
                }
            }
            else
            {
                ClearTextBoxes(5);
            }
            TextBox1.Text = getFinalParam(0).ToString();
            TextBox2.Text = (getFinalParam(1) + 1).ToString();
            double speed = getFinalParam(2) + 1;
            if (speed < 1.3 * Math.Sqrt(speed + 11))
            {
                speed = 1.3 * Math.Sqrt(speed + 11);
            }
            TextBox3.Text = speed.ToString();
            TextBox4.Text = (getFinalParam(3) + 1).ToString();

            double count1 = 0;
            foreach (Symptom symptom in symptomsList)
            {
                if (symptom.isActive) count1++;
            }

            
            switch (int.Parse(TextBox4.Text) - count1)
            {
                case 2: VirusTransmission.Text = "Контакт";
                    break;
                case 3: VirusTransmission.Text = "Контакт";
                    break;
                default:
                    if (int.Parse(TextBox4.Text) - count1  <= 1) VirusTransmission.Text = "Кровь";
                    else VirusTransmission.Text = "По воздуху";
                    break;
            }

            int Res = (int)( int.Parse(TextBox2.Text) * 1.0 - count1 / 2);
            switch (Res)
            {
                case 2: 
                    VirusResistance.Text = "Сахар";
                    break;
                case 3:
                    VirusResistance.Text = "Апельсиновый сок";
                    break;
                case 4:
                    VirusResistance.Text = "Спейсацилин";
                    break;
                case 5:
                    VirusResistance.Text = "Физраствор";
                    break;
                case 6:
                    VirusResistance.Text = "Этанол";
                    break;
                case 7:
                    VirusResistance.Text = "Тепорон";
                    break;
                case 8: 
                    VirusResistance.Text = "Дифенгидрамин";
                    break;
                case 9:
                    VirusResistance.Text = "Липолицид";
                    break;
                case 10:
                    VirusResistance.Text = "Серебро";
                    break;
                default:
                    if (Res <= 1) VirusResistance.Text = "Соль";
                    else VirusResistance.Text = "Золото";
                    break;
            }

            if (int.Parse(TextBox1.Text) <= 1) TextBoxStealth.Text = "Анализатор";
            else if (int.Parse(TextBox1.Text) == 2) TextBoxStealth.Text = "Пандемик";
            else TextBoxStealth.Text = "Ничто";

        }

        private int getFinalParam(int index)
        {
            int buff = 0;
            int i = 0;
            foreach (List<TextBox> list in textBoxList)
            {
                if (textBoxList[i][index].Text != "")
                {
                    buff += int.Parse(textBoxList[i][index].Text);
                }
                i++;
            }
            return buff;
        }

        private void CloseAction(ref ComboBox comboBox, ref Button button, int index)
        {
            virusesListActiveUpdate(ref comboBox, index);
            if (comboBox.SelectedIndex != -1)
            {
                button.IsEnabled = true;
                comboBox.IsEnabled = false;
            }
        }       

        void ComboBox1_DropDownOpened(object sender, EventArgs e)
        {
            
            virusesListActiveUpdate(ref ComboBox1, 1);
        }

        void ComboBox1_DropDownClosed(object sender, EventArgs e)
        {
            CloseAction(ref ComboBox1, ref ClearButton1, 1);
        }

        void ComboBox2_DropDownOpened(object sender, EventArgs e)
        {
            virusesListActiveUpdate(ref ComboBox2, 2);
        }

        void ComboBox2_DropDownClosed(object sender, EventArgs e)
        {
            CloseAction(ref ComboBox2, ref ClearButton2, 2);
        }

        void ComboBox3_DropDownOpened(object sender, EventArgs e)
        {
            
            virusesListActiveUpdate(ref ComboBox3, 3);
        }

        void ComboBox3_DropDownClosed(object sender, EventArgs e)
        {
            CloseAction(ref ComboBox3, ref ClearButton3, 3);
        }

        void ComboBox4_DropDownOpened(object sender, EventArgs e)
        {
            
            virusesListActiveUpdate(ref ComboBox4, 4);
        }

        void ComboBox4_DropDownClosed(object sender, EventArgs e)
        {
            CloseAction(ref ComboBox4, ref ClearButton4, 4);
        }

        void ComboBox5_DropDownOpened(object sender, EventArgs e)
        {
            
            virusesListActiveUpdate(ref ComboBox5, 5);
        }

        void ComboBox5_DropDownClosed(object sender, EventArgs e)
        {
            CloseAction(ref ComboBox5, ref ClearButton5, 5);
        }

        void ComboBox6_DropDownOpened(object sender, EventArgs e)
        {
            
            virusesListActiveUpdate(ref ComboBox6, 6);
        }

        void ComboBox6_DropDownClosed(object sender, EventArgs e)
        {
            CloseAction(ref ComboBox6, ref ClearButton6, 6);
        }

        private void ClearClickUpdate(ref ComboBox comboBox, Button button)
        {
            zeroUpdate(ref comboBox);
            comboBox.IsEnabled = true;
            button.IsEnabled = false;
        }

        private void ClearButton1_Click(object sender, RoutedEventArgs e)
        {
            ClearClickUpdate(ref ComboBox1, ClearButton1);
        }

        private void ClearButton2_Click(object sender, RoutedEventArgs e)
        {
            ClearClickUpdate(ref ComboBox2, ClearButton2);
        }

        private void ClearButton3_Click(object sender, RoutedEventArgs e)
        {
            ClearClickUpdate(ref ComboBox3, ClearButton3);
        }

        private void ClearButton4_Click(object sender, RoutedEventArgs e)
        {
            ClearClickUpdate(ref ComboBox4, ClearButton4);
        }

        private void ClearButton5_Click(object sender, RoutedEventArgs e)
        {
            ClearClickUpdate(ref ComboBox5, ClearButton5);
        }

        private void ClearButton6_Click(object sender, RoutedEventArgs e)
        {
            ClearClickUpdate(ref ComboBox6, ClearButton6);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string outStr = "";
            outStr += VirusTarget.Text +"\n";
            int i = 1;
            foreach (Symptom symptom in symptomsList)
            {
                if (symptom.isActive)
                {
                    outStr += i + ") " + symptom.name + " - " + symptom.effect + "\n";
                    i++;
                }
            }
            FFT.Text = outStr;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string outStr = "";
            outStr += "[grid][row][cell]     [logo][cell][small]Форма NT-MD-04[/small]\n" +
                    "[br][b][large]Сводка о вирусе[/large][/b]\n" +
                    "[grid][cell]Научная Станция Nanotrasen \"Kerberos\" [cell] Медецинский отдел[/grid][row][/grid]\n" +
                    "[center][small]Перед заполнением прочтите от начала до конца | Во всех PDA имеется ручка[/small][/center]\n" +
                    "[hr]\n" +
                    "[center][i][large][b]Вирус: " + VirusName.Text + " [field][/b][/large][/i][/center]\n" +
                    "[i][br]Полное название вируса: " + VirusName.Text + " [field]\n" +
                    "[br]Задачи вируса: " + VirusTarget.Text + " [field]\n" +
                    "[br]Передача вируса: " + VirusTransmission.Text + " [field]\n" +
                    "[br]Побочные эффекты:\n";
            string buff = "";
            int i = 1;
            foreach (Symptom symptom in symptomsList)
            {
                if (symptom.isActive)
                {
                    buff += i + ") " + symptom.name + " - " + symptom.effect + "\n";
                    i++;
                }
            }
            outStr += buff + "[field]\n";
            outStr += "Дополнительная информация: [field]\n" +
                    "Лечение вируса: " + VirusResistance.Text + "[field][/i]\n\n" +
                    "[hr]\n" +
                    "[center][i][large][b]Подписи и штампы[/b][/large][/i][/center]\n" +
                    "[i]Подпись вирусолога: [sign][field][/i]\n\n" +
                    "[hr]\n" +
                    "[i][small]*В дополнительной информации, указывается вся остальная информация, по поводу данного вируса.[/small][/i]\n\n" +
                    "[hr]\n" +
                    "[center][i][small]Подписи Глав являются доказательством их согласия.\n" +
                    "Данный документ является недействительным при отсутствии релевантной печати.[/small][/i][/center]";
            FFT.Text = outStr;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string outStr = "";
            outStr += "[grid][row][cell]     [logo][cell][small]Форма NT-MD-04[/small]\n" +
                    "[br][b][large]Запрос на распространение вируса[/large][/b]\n" +
                    "[grid][cell]Научная Станция Nanotrasen \"Kerberos\" [cell] Медецинский отдел[/grid][row][/grid]\n" +
                    "[center][small]Перед заполнением прочтите от начала до конца | Во всех PDA имеется ручка[/small][/center]\n" +
                    "[hr]\n" +
                    "[center][i][large][b]Основная информация[/b][/large][/i][/center]\n" +
                    "[br]Я, [field], в должности – вирусолога, запрашиваю право на распространение вируса среди экипажа станции.\n" +
                    "[br]Название вируса: " + VirusName.Text + " [field]" + 
                    "[br][br]Задачи вируса: " + VirusTarget.Text + " [field]" + 
                    "[br][br]Лечение: " + VirusResistance.Text + "[field]\n" +
                    "[grid][br][cell]Вакцина была произведена [br]и в данный момент находится: [cell]На стойке мед. отдела[field][/grid]\n\n" +
                    "[hr]\n" +
                    "[center][i][large][b]Подписи и штампы[/b][/large][/i][/center]\n" +
                    "[i]Подпись вирусолога: [sign][field][/i]\n" +
                    "[i]Подпись Глав. Врача: [field][/i]\n" +
                    "[i]Подпись Капитана: [field][/i]\n\n" +
                    "[hr]\n" +
                    "[small]*Производитель вируса несет полную ответственность за его распространение, изолирование и лечение *При возникновении опасных или смертельных побочных эффектов у членов экипажа, производитель должен незамедлительно предоставить вакцину, от данного вируса.[/small]\n\n" +
                    "[hr]\n" +
                    "[center][i][small]Подписи Глав являются доказательством их согласия.\n" +
                    "Данный документ является недействительным при отсутствии релевантной печати.[/small][/i][/center]";
            FFT.Text = outStr;
        }



        private void SortBySpeed_Click(object sender, RoutedEventArgs e)
        {
            symptomsList.Sort((x, y) => x.speed.CompareTo(y.speed));
        }

        private void GetSymptoms()
        {
            var request = new GetRequest("https://wiki.ss220.space/index.php/%D0%A0%D1%83%D0%BA%D0%BE%D0%B2%D0%BE%D0%B4%D1%81%D1%82%D0%B2%D0%BE_%D0%BF%D0%BE_%D0%B2%D0%B8%D1%80%D1%83%D1%81%D0%BE%D0%BB%D0%BE%D0%B3%D0%B8%D0%B8#Sensory_Restoration");
            request.Run();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Stream fs;
            BinaryFormatter formatter = new BinaryFormatter();
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "dat files (*.dat)|*.dat";
            saveFileDialog.DefaultExt = ".dat";
            saveFileDialog.InitialDirectory = Environment.CurrentDirectory + "\\completeViruses";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if ((bool)saveFileDialog.ShowDialog())
            {
                if ((fs = saveFileDialog.OpenFile()) != null)
                {
                    formatter.Serialize(fs, symptomsList);
                    fs.Close();
                }
            }
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.FileName = "Document";
            openFileDialog.DefaultExt = ".dat";
            openFileDialog.Filter = "Dat Files (.dat)|*.dat";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory + "\\completeViruses";

#pragma warning disable CS8629 // Тип значения, допускающего NULL, может быть NULL.
            if ((bool)openFileDialog.ShowDialog())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = openFileDialog.OpenFile() as FileStream;
                symptomsList = (List<Symptom>)formatter.Deserialize(stream);
                updateComboBoxes();
                textBoxesUpdate();
                string filename = openFileDialog.FileName;
            }
#pragma warning restore CS8629 // Тип значения, допускающего NULL, может быть NULL.

        }

        private void updateComboBoxes()
        {
            int comboBoxNumber = 0;
            foreach (Symptom symptom in symptomsList)
            {
                if (symptom.isActive)
                {
                    ComboBox comboBox = comboBoxList[comboBoxNumber];
                    comboBox.SelectedItem = symptom.name;
                    comboBoxNumber++;
                }
            }
        }
    }
}
