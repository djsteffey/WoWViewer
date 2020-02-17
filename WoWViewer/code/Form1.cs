using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CASCLib;

namespace WoWViewer
{
    public partial class Form1 : Form
    {
        // variables
        CASCHandler casc_handler = null;
        CASCFolder casc_root_folder = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            string mode = args[0];
            string pattern = args[1];
            string dest = args[2];
            LocaleFlags locale = (LocaleFlags)Enum.Parse(typeof(LocaleFlags), args[3]);
            bool overrideArchive = bool.Parse(args[4]);

            cascHandler.Root.LoadListFile(Path.Combine(Environment.CurrentDirectory, "listfile.csv"), bgLoader);
            CASCFolder root = cascHandler.Root.SetFlags(locale, overrideArchive);
            //cascHandler.Root.MergeInstall(cascHandler.Install);

            Console.WriteLine("Loaded.");

            Console.WriteLine("Extract params:");
            Console.WriteLine("    Mode: {0}", mode);
            Console.WriteLine("    Pattern/Listfile: {0}", pattern);
            Console.WriteLine("    Destination: {0}", dest);
            Console.WriteLine("    LocaleFlags: {0}", locale);
            Console.WriteLine("    OverrideArchive: {0}", overrideArchive);

            if (mode == "pattern")
            {
                Wildcard wildcard = new Wildcard(pattern, true, RegexOptions.IgnoreCase);

                foreach (var file in CASCFolder.GetFiles(root.Entries.Select(kv => kv.Value)))
                {
                    if (wildcard.IsMatch(file.FullName))
                        ExtractFile(cascHandler, file.Hash, file.FullName, dest);
                }
            }
            else if (mode == "listfile")
            {
                if (cascHandler.Root is WowRootHandler wowRoot)
                {
                    char[] splitChar = new char[] { ';' };

                    var names = File.ReadLines(pattern).Select(s => s.Split(splitChar, 2)).Select(s => new { id = int.Parse(s[0]), name = s[1] });

                    foreach (var file in names)
                        ExtractFile(cascHandler, wowRoot.GetHashByFileDataId(file.id), file.name, dest);
                }
                else
                {
                    var names = File.ReadLines(pattern);

                    foreach (var file in names)
                        ExtractFile(cascHandler, 0, file, dest);
                }
            }

            Console.WriteLine("Extracted.");
            */
            // add root casc node
            this.m_treeview.Nodes.Add("CASC");
        }

        private static void ExtractFile(CASCHandler cascHandler, ulong hash, string file, string dest)
        {
            Console.Write("Extracting '{0}'...", file);

            try
            {
                if (hash != 0)
                    cascHandler.SaveFileTo(hash, dest, file);
                else
                    cascHandler.SaveFileTo(file, dest);

                Console.WriteLine(" Ok!");
            }
            catch (Exception exc)
            {
                Console.WriteLine($" Error ({exc.Message})!");
                Logger.WriteLine(exc.Message);
            }
        }

        private void BgLoader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.m_listbox.Items.Add("Progress..." + e.ProgressPercentage);
        }

        private void m_button_Click(object sender, EventArgs e)
        {
            // signal we are loading
            this.m_listbox.Items.Add("Loading...");

            // load in a thread
            new Thread(load_thread_function).Start();
        }

        private void load_thread_function()
        {
            // message
            this.append_to_listbox("Loading CASC");

            // background worker for the casc handler
            BackgroundWorkerEx bgLoader = new BackgroundWorkerEx();

            // config using local storage
            CASCConfig config = CASCConfig.LoadLocalStorageConfig("C:/Program Files (x86)/World of Warcraft/");

            // open the casc
            this.append_to_listbox("Opening Storage");
            this.casc_handler = CASCHandler.OpenStorage(config, bgLoader);

            // load the list file
            this.append_to_listbox("Loading listfile");
            this.casc_handler.Root.LoadListFile(Path.Combine(Environment.CurrentDirectory, "listfile.csv"));

            // get the root folder
            this.casc_root_folder = this.casc_handler.Root.SetFlags(LocaleFlags.enUS, false);
            this.m_treeview.Nodes[0].Tag = this.casc_root_folder;

            // add all the children of the root
            foreach (var entry in this.casc_root_folder.Entries)
            {
                TreeNode child = new TreeNode(entry.Key);
                child.Tag = entry.Value;
                this.add_node_to_node(this.m_treeview.Nodes[0], child);
            }

            this.append_to_listbox("complete");
        }

        private void m_treeview_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag is CASCFolder)
            {
                CASCFolder folder_expanding = e.Node.Tag as CASCFolder;
                e.Node.Nodes.Clear();
                foreach (var entry in folder_expanding.Entries)
                {
                    TreeNode child = new TreeNode(entry.Key);
                    child.Tag = entry.Value;
                    if (entry.Value is CASCFolder)
                    {
                        child.Nodes.Add("temp");
                    }
                    this.add_node_to_node(e.Node, child);
                }
            }
            else if (e.Node.Tag is CASCFile)
            {

            }
        }

        private void append_to_listbox(string txt)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(append_to_listbox), new object[] { txt });
                return;
            }
            else
            {
                this.m_listbox.Items.Add(txt);
            }
        }

        private void add_node_to_node(TreeNode parent, TreeNode child)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<TreeNode, TreeNode>(add_node_to_node), new object[] { parent, child });
            }
            else
            {
                parent.Nodes.Add(child);
            }
        }

        private void clear_node(TreeNode node)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<TreeNode>(clear_node), new object[] { node });
            }
            else
            {
                node.Nodes.Clear();
            }
        }
    }
}
