using Android;
using Android.App;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Text.RegularExpressions;

namespace Ynote
{
    [Activity(Label = "Ynote", Theme = "@style/AppTheme", MainLauncher = false)]
    public class PostPrivateNoteActivity : AppCompatActivity
    {
        MediaRecorder MediaRecorder;
        string RecordedAudioPath;
        string[] requiredPermissions = new String[] { Manifest.Permission.RecordAudio, Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.new_note_layout);
            FindViewById<Button>(Resource.Id.btnPostPrivateNote).Click += Insert;

            FindViewById<Button>(Resource.Id.btncolor8).Click += Color8;
            FindViewById<Button>(Resource.Id.btncolor9).Click += Color9;
            FindViewById<Button>(Resource.Id.btncolor10).Click += Color10;
            FindViewById<Button>(Resource.Id.btncolor11).Click += Color11;

            FindViewById<Button>(Resource.Id.btnbacktoprofile).Click += (s,args)=> {
                try
                {
                    MediaRecorder.Stop(); MediaRecorder.Release(); MediaRecorder.Dispose(); MediaRecorder = null;
                    Toast.MakeText(this, "Recording was cancelled", ToastLength.Short).Show();
                }
                catch (System.NullReferenceException)
                {
                    StartActivity(typeof(MainActivity));

                }
            };
            FindViewById<EditText>(Resource.Id.privatenotetext).TextChanged += OnTextChange;
            FindViewById<EditText>(Resource.Id.txtPrivateNoteTagName).TextChanged += OnTextTagChange;
            FindViewById<EditText>(Resource.Id.PrivateNoteBorderColor).TextChanged += OnTextBorderColorChange;
            FindViewById<Button>(Resource.Id.btnRecordingAction).Click += NewRecord;




            // Create your application here
        }
        void Color8(object sender, EventArgs e)
        {
            FindViewById<EditText>(Resource.Id.PrivateNoteBorderColor).Text = "#3D1054";
        }
        void Color9(object sender, EventArgs e)
        {
            FindViewById<EditText>(Resource.Id.PrivateNoteBorderColor).Text = "#FC1E50";
        }
        void Color10(object sender, EventArgs e)
        {
            FindViewById<EditText>(Resource.Id.PrivateNoteBorderColor).Text = "#FAD961";
        }
        void Color11(object sender, EventArgs e)
        {
            FindViewById<EditText>(Resource.Id.PrivateNoteBorderColor).Text = "#623FD0";
        }

        private void PrepareSetup()
        {
            MediaRecorder = new MediaRecorder();
            MediaRecorder.SetAudioSource(AudioSource.Mic);
            MediaRecorder.SetOutputFormat(OutputFormat.Mpeg4);
            MediaRecorder.SetAudioEncoder(AudioEncoder.HeAac);
            MediaRecorder.SetAudioSamplingRate(16000);

            MediaRecorder.SetOutputFile(RecordedAudioPath);
        }
        bool IsAllPermissionEnabled()
        {

            foreach (var permission in requiredPermissions)
            {
                if (ActivityCompat.CheckSelfPermission(this, permission) != Android.Content.PM.Permission.Granted)
                {
                    return false;
                }

            }
            return true;
        }

        void NewRecord(object sender, EventArgs e)
        {
            try
            {
                if (IsAllPermissionEnabled())
                {
                    _ = System.IO.Directory.CreateDirectory($"{Android.OS.Environment.ExternalStorageDirectory.AbsolutePath}/Ynote/Audio");

                    RecordedAudioPath = $"{$"{Android.OS.Environment.ExternalStorageDirectory.AbsolutePath}/Ynote/Audio"}{$"/record{new SQLiteConnection(CONNECTION.DBPath).Table<Note>().Count() + 1.ToString()}.mp3"}";
                    PrepareSetup();
                    MediaRecorder.Prepare();
                    MediaRecorder.Start();
                    FindViewById<Button>(Resource.Id.btnRecordingAction).Text = "Recording...";

                    FindViewById<Button>(Resource.Id.btnRecordingAction).SetCompoundDrawablesWithIntrinsicBounds(_ = GetDrawable(Resource.Drawable.icon_mic_active), null, null, null);
                    FindViewById<Button>(Resource.Id.btnRecordingAction).SetTextColor(Android.Graphics.Color.ParseColor("#55cf90"));
                    FindViewById<Button>(Resource.Id.btnRecordingAction).SetTypeface(FindViewById<Button>(Resource.Id.btnRecordingAction).Typeface, Android.Graphics.TypefaceStyle.Italic);
                    Toast.MakeText(this, "Click save to when you are done", ToastLength.Short).Show();

                }
                else
                {
                    Snackbar.Make(FindViewById<LinearLayout>(Resource.Id.newnotelinear),
                  "Ynote is requesting Mic and Storage permission to use this feature.",
                  Snackbar.LengthIndefinite)
                                 .SetAction(Android.Resource.String.Ok,
                      new Action<View>(delegate (View obj) {
                          ActivityCompat.RequestPermissions(this, requiredPermissions, 1000);

                      }
                            )
                                 ).Show();
                  
                        

                    
                    

                }
            }
            catch (Exception ee)
            {
                Toast.MakeText(this, ee.Message, ToastLength.Short).Show();
            }

        }





        //84e2f7
        protected static bool CheckValidFormatHtmlColor(string inputColor)
        {


            var result = System.Drawing.Color.FromName(inputColor);
            return result.IsKnownColor;
        }

        void Insert(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(FindViewById<EditText>(Resource.Id.privatenotetext).Text))
            {
                Toast.MakeText(this, "Note is required", ToastLength.Short).Show();

            }

            else if (string.IsNullOrWhiteSpace(FindViewById<EditText>(Resource.Id.PrivateNoteBorderColor).Text))
            {
                Toast.MakeText(this, "Pick a color please", ToastLength.Short).Show();
            }

            else
            {




                try
                {
                    if (MediaRecorder != null)
                    {
                        MediaRecorder.Stop();

                        MediaRecorder.Release();
                        MediaRecorder = null;

                    }

                    Note note = new Note(new SQLiteConnection(CONNECTION.DBPath).Table<Note>().Count() + 1,
                            FindViewById<EditText>(Resource.Id.PrivateNoteBorderColor).Text,

                       FindViewById<EditText>(Resource.Id.privatenotetext).Text

                       , DateTime.Now.ToString("MMMM dd,yyyy"),

                       FindViewById<CheckBox>(Resource.Id.chkImportant).Checked == true ? "Yes" : "No",

                      string.IsNullOrWhiteSpace(FindViewById<EditText>(Resource.Id.txtPrivateNoteTagName).Text)
                       ? "#UnTagged"
                       : FindViewById<EditText>(Resource.Id.txtPrivateNoteTagName).Text.StartsWith("#") ?
                       FindViewById<EditText>(Resource.Id.txtPrivateNoteTagName).Text : "#" + FindViewById<EditText>(Resource.Id.txtPrivateNoteTagName).Text,



                      FindViewById<EditText>(Resource.Id.notePassword).Text, null,
                    string.IsNullOrWhiteSpace(FindViewById<EditText>(Resource.Id.noteTitle).Text) ? "Untitled" :
                   FindViewById<EditText>(Resource.Id.noteTitle).Text, RecordedAudioPath);
                    Snackbar.Make(FindViewById<RelativeLayout>(Resource.Id.RlayoutNote), "Saving your note...", 500).Show();

                    _ = new SQLiteConnection(CONNECTION.DBPath).Insert(note);
                   
                    

                    StartActivity(typeof(MainActivity));
                    Snackbar.Make(FindViewById<Android.Support.Design.Internal.FlowLayout>(Resource.Id.ProfileLinearNotes)
                        , "Your note has been created!", 500).Show();
                }
                catch (Exception)
                {

                }


            }

        }
       
        void OnTextBorderColorChange(object sender, EventArgs e)
        {
            try
            {
                if (!CheckValidFormatHtmlColor(FindViewById<EditText>(Resource.Id.PrivateNoteBorderColor).Text))
                {
                    FindViewById<EditText>(Resource.Id.PrivateNoteBorderColor).SetBackgroundColor(Android.Graphics.Color.ParseColor(
                        FindViewById<EditText>(Resource.Id.PrivateNoteBorderColor).Text));
                }
            }
            catch (Exception)
            {

            }
        }
        void OnTextChange(object sender, EventArgs e)
        {
            
                FindViewById<Button>(Resource.Id.btnPostPrivateNote).Enabled =
               !(FindViewById<EditText>(Resource.Id.privatenotetext).Text is null);
           
           
        }
        void OnTextTagChange(object sender, EventArgs e)
        {
            FindViewById<Button>(Resource.Id.btnPostPrivateNote).Enabled =
               !(FindViewById<EditText>(Resource.Id.txtPrivateNoteTagName).Text is null);
        }

        protected override void OnPause()
        {
            base.OnPause();
            switch (MediaRecorder)
            {
                case null:
                    break;
                default:
                 
                    
                    Toast.MakeText(this, "Recording in background...", ToastLength.Short).Show();
                    break;
            }
        }
        protected override void OnResume()
        {
            base.OnResume();
            if (MediaRecorder != null)
            {
                Toast.MakeText(this, "Still recording...", ToastLength.Short).Show();
            }
        }


    }
}