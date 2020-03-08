
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using SQLite;
using Ynote;

namespace Ynote
{
    [Activity(Label = "NoteEditorActivity")]
    public class NoteEditorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.note_editor_layout);
            FindViewById<EditText>(Resource.Id.txtNoteEditor).Text = INFONOTE.NoteText;
            FindViewById<EditText>(Resource.Id.txtNotePass).Text = INFONOTE.Password;
            FindViewById<TextView>(Resource.Id.txtDate).Text = string.IsNullOrWhiteSpace(INFONOTE.DateModified)
                ? " Created: " + INFONOTE.NoteTime
                : " Created: " + INFONOTE.NoteTime + "\t\t\tModified: " + INFONOTE.DateModified;
            FindViewById<EditText>(Resource.Id.txtNoteTag).Text = INFONOTE.Tag;
          
                FindViewById<EditText>(Resource.Id.txtNoteTitle).Text = INFONOTE.NoteTitle;
            if (string.IsNullOrWhiteSpace(INFONOTE.AudioPath))
            {
                FindViewById<Button>(Resource.Id.btnPLayRecord).Visibility = Android.Views.ViewStates.Gone;
                FindViewById<View>(Resource.Id.viewRecord).Visibility = Android.Views.ViewStates.Gone;

            }
            else
            {
                FindViewById<Button>(Resource.Id.btnPLayRecord).Visibility = Android.Views.ViewStates.Visible;
                FindViewById<View>(Resource.Id.viewRecord).Visibility = Android.Views.ViewStates.Visible;
            }




            FindViewById<Button>(Resource.Id.btnRestore).Click += (s, args) =>
            {
                FindViewById<EditText>(Resource.Id.txtNoteEditor).Text = INFONOTE.NoteText;
                FindViewById<EditText>(Resource.Id.txtNoteTitle).Text = INFONOTE.NoteTitle;
                FindViewById<EditText>(Resource.Id.txtNoteTag).Text = INFONOTE.Tag;
                FindViewById<EditText>(Resource.Id.txtNotePass).Text = INFONOTE.Password;

            };
            FindViewById<Button>(Resource.Id.btnSave).Click += (s, args) =>
            {
                _ = new SQLiteConnection(CONNECTION.DBPath).Table<Note>().Delete(x => x.NoteText == INFONOTE.NoteText);

                Note note = new Note(INFONOTE.NoteID,

                        INFONOTE.NoteColor,

FindViewById<EditText>(Resource.Id.txtNoteEditor).Text

                   ,INFONOTE.NoteTime 
                    ,

                   INFONOTE.Important,

                FindViewById<EditText>(Resource.Id.txtNoteTag).Text.StartsWith("#")
                   ? FindViewById<EditText>(Resource.Id.txtNoteTag).Text
                   : "#" + FindViewById<EditText>(Resource.Id.txtNoteTag).Text,

              FindViewById<EditText>(Resource.Id.txtNotePass).Text, System.DateTime.Now.ToString("MMMM dd,yyyy"),
              FindViewById<EditText>(Resource.Id.txtNoteTitle).Text,INFONOTE.AudioPath);
                _ = new SQLiteConnection(CONNECTION.DBPath).Insert(note);
             
                Toast.MakeText(this, "Updated", ToastLength.Short).Show();
                StartActivity(typeof(MainActivity));


            };
            
            FindViewById<Button>(Resource.Id.btnPLayRecord).Click += (s, args) =>
            {
                using Android.Media.MediaPlayer mediaPlayer = new Android.Media.MediaPlayer();
                if (FindViewById<Button>(Resource.Id.btnPLayRecord).Text == "Play")
                {
                    FindViewById<Button>(Resource.Id.btnPLayRecord).SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(Resource.Drawable.icon_mic_playxml), null, null, null);

                    FindViewById<Button>(Resource.Id.btnPLayRecord).SetBackgroundResource(Resource.Drawable.active_button);
                    FindViewById<Button>(Resource.Id.btnPLayRecord).SetTextColor(Android.Graphics.Color.White);
                    FindViewById<Button>(Resource.Id.btnPLayRecord).SetTextSize(Android.Util.ComplexUnitType.Sp, 15);
                    FindViewById<Button>(Resource.Id.btnPLayRecord).Text = "Playing...";

                    mediaPlayer.SetDataSource(INFONOTE.AudioPath);

                    mediaPlayer.Prepare();
                    mediaPlayer.Start();
                }
                else if(FindViewById<Button>(Resource.Id.btnPLayRecord).Text == "Playing...")
                {
                    FindViewById<Button>(Resource.Id.btnPLayRecord).SetBackgroundResource(Resource.Drawable.btn_mic_paused);
                    FindViewById<Button>(Resource.Id.btnPLayRecord).SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(Resource.Drawable.icon_mic_paused),null,null,null);

                    FindViewById<Button>(Resource.Id.btnPLayRecord).SetTextColor(Android.Graphics.Color.White);
                    FindViewById<Button>(Resource.Id.btnPLayRecord).SetTextSize(Android.Util.ComplexUnitType.Sp, 15);
                    FindViewById<Button>(Resource.Id.btnPLayRecord).Text = "Paused";
                    mediaPlayer.Stop();
                }

                else if (FindViewById<Button>(Resource.Id.btnPLayRecord).Text == "Paused")
                {
                    FindViewById<Button>(Resource.Id.btnPLayRecord).SetBackgroundResource(Resource.Drawable.active_button);
                    FindViewById<Button>(Resource.Id.btnPLayRecord).SetTextColor(Android.Graphics.Color.White);
                    FindViewById<Button>(Resource.Id.btnPLayRecord).SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(Resource.Drawable.icon_mic_playxml), null, null, null);

                    FindViewById<Button>(Resource.Id.btnPLayRecord).SetTextSize(Android.Util.ComplexUnitType.Sp, 15);
                    FindViewById<Button>(Resource.Id.btnPLayRecord).Text = "Playing...";

                  
                    mediaPlayer.Start();
                }
                mediaPlayer.Completion += (s, args) =>
                {

                    FindViewById<Button>(Resource.Id.btnPLayRecord).SetBackgroundResource(Resource.Drawable.insidebuttons);
                    FindViewById<Button>(Resource.Id.btnPLayRecord).SetTextColor(Android.Graphics.Color.ParseColor("#000000"));
                        FindViewById<Button>(Resource.Id.btnPLayRecord).SetTextSize(Android.Util.ComplexUnitType.Sp, 18);

                        FindViewById<Button>(Resource.Id.btnPLayRecord).Text = "Play";
                    
                };
              

            };

            FindViewById<Button>(Resource.Id.btnSizeUp).Click += (s, args) =>
            {
                FindViewById<EditText>(Resource.Id.txtNoteEditor).SetTextSize(Android.Util.ComplexUnitType.Px,
                   FindViewById<EditText>(Resource.Id.txtNoteEditor).TextSize + 1.5f); 
              
                };
            FindViewById<Button>(Resource.Id.btnSizeDown).Click += (s, args) =>
            {

                FindViewById<EditText>(Resource.Id.txtNoteEditor).SetTextSize(Android.Util.ComplexUnitType.Px,
                        FindViewById<EditText>(Resource.Id.txtNoteEditor).TextSize - 1.5f);
            };


            FindViewById<Button>(Resource.Id.btnSelectAll).Click += (s, args) =>
            {
                FindViewById<EditText>(Resource.Id.txtNoteEditor).RequestFocus();
                FindViewById<EditText>(Resource.Id.txtNoteEditor).SelectAll();
            };

            FindViewById<Button>(Resource.Id.btnCopy).Click += (s, args) =>
            {
                ((ClipboardManager)base.GetSystemService(ClipboardService)).PrimaryClip 
                    = ClipData.NewPlainText("", FindViewById<EditText>(Resource.Id.txtNoteEditor).Text);
                Toast.MakeText(this, "Copied!", ToastLength.Short).Show();
            };


            FindViewById<Button>(Resource.Id.btnCenter).Click += (s, args) =>
             {
                 if (FindViewById<EditText>(Resource.Id.txtNoteEditor).Gravity == (Android.Views.GravityFlags.Top | Android.Views.GravityFlags.Center))
                 {
                     FindViewById<Button>(Resource.Id.btnCenter).SetBackgroundResource(Resource.Drawable.active_button);
                     FindViewById<EditText>(Resource.Id.txtNoteEditor).Gravity = Android.Views.GravityFlags.Top | Android.Views.GravityFlags.Left;

                 }
                 else
                 {
                     FindViewById<EditText>(Resource.Id.txtNoteEditor).Gravity = Android.Views.GravityFlags.Top | Android.Views.GravityFlags.Center;
                     FindViewById<Button>(Resource.Id.btnCenter).SetBackgroundResource(Resource.Drawable.insidebuttons);
                     
                 }


             };
            // Create your application here
        }
    }
}