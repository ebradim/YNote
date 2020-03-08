using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Card;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using SQLite;
using Ynote;

namespace Ynote
{
    [Activity(Label = "SearchActivity")]
    public class SearchActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.search_layout);
            FindViewById<Button>(Resource.Id.btnSearchClickFindTagTexT).Click += INFONOTEs;
            FindViewById<EditText>(Resource.Id.SearchTxt).Text += INFONOTE.NoteText;
            FindViewById<Button>(Resource.Id.btnSearchClickFindTagTexT).PerformClick();
            FindViewById<Android.Support.V4.Widget.SwipeRefreshLayout>(Resource.Id.swipSearch).Refresh += INFONOTEs;
            // Create your application here
        }


        void INFONOTEs(object sender , EventArgs e)
        {
        
            FindViewById<LinearLayout>(Resource.Id.SearchResultLinear).RemoveAllViews();
            foreach (Note note in from Note item in new SQLiteConnection(CONNECTION.DBPath).Table<Note>().Reverse()
                                 let note = new Note(item.num, item.NoteColor, item.NoteText, item.NoteTime, item.Important, item.Tag,item.Password,item.DateModified,item.NoteTitle,item.AudioPath)
                                 select note)
            {
                INFONOTE.NoteColor = note.NoteColor;
                INFONOTE.Tag = note.Tag;
                INFONOTE.NoteText = note.NoteText;
                INFONOTE.NoteTime = note.NoteTime;
                INFONOTE.Important = note.Important;
                INFONOTE.Password = note.Password;
                INFONOTE.DateModified = note.DateModified;
                INFONOTE.NoteTitle = note.NoteTitle;
                INFONOTE.AudioPath = note.AudioPath;

                if (INFONOTE.NoteText.Length > 130)
                {
                    INFONOTE.NoteText = $"{INFONOTE.NoteText.Substring(0, 110)}...";

                }
                if (FindViewById<EditText>(Resource.Id.SearchTxt).Text.StartsWith("#"))
                {
                    if (INFONOTE.Tag.ToLower().Contains(FindViewById<EditText>(Resource.Id.SearchTxt).Text.ToLower()))
                    {
                        InitializeControlsForBottomViewNotes();

                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if (INFONOTE.NoteText.ToLower().Contains(FindViewById<EditText>(Resource.Id.SearchTxt).Text.ToLower())||
                       INFONOTE.NoteTitle.ToLower().Contains(FindViewById<EditText>(Resource.Id.SearchTxt).Text.ToLower())) 

                    {

                        InitializeControlsForBottomViewNotes();


                    }
                    else
                    {
                        continue;
                    }


                }
            }
            FindViewById<Android.Support.V4.Widget.SwipeRefreshLayout>(Resource.Id.swipSearch).Refreshing = false;

        }




        //            

        private void InitializeControlsForBottomViewNotes()
        {
            Typeface ArialNB = Typeface.CreateFromAsset(Assets, "fonts/ARIALNB.TTF");
            Typeface Robot_Regular = Typeface.CreateFromAsset(Assets, "fonts/Exo2RegularCondensed.otf");
            Typeface Segeo = Typeface.CreateFromAsset(Assets, "fonts/Segoemdl2assets.ttf");


            var ll = FindViewById<LinearLayout>(Resource.Id.SearchResultLinear);



            /*############################################################################################################################*/
            /*############################################################################################################################*/
            LinearLayout.LayoutParams psp = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent,
               250);
            psp.SetMargins(20, 10, 20, 10);
            MaterialCardView card = new MaterialCardView(this);


            card.SetCardBackgroundColor(Color.ParseColor("#fafafa"));




            card.Radius = 25;
            card.Elevation = 0;


            card.StrokeColor = Color.ParseColor("#f5f5f5");



            card.StrokeWidth = 4;
            card.LayoutParameters = psp;
            card.SetPadding(15, 0, 15, 15);
            /*#######################################################################################################################################*/
            /*#######################################################################################################################################*/




            /*#######################################################################################################################################*/
            /*############################################################################################################################*/



            /*######################################################################################################################*/
            /*############################################################################################################################*/


            /*#######################################################################################################################*/
            /*############################################################################################################################*/




            TextView btnTag = new TextView(this);
            btnTag.Text = INFONOTE.Tag;


            btnTag.SetBackgroundColor(Color.ParseColor("#f5f5f5"));


            btnTag.TextSize = 12;
            btnTag.SetAllCaps(false);
            btnTag.SetX(0);
            btnTag.SetY(0);
            btnTag.SetPadding(0, 0, 0, 10);

            btnTag.Typeface = ArialNB;
            btnTag.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, 57);
            switch (INFONOTE.NoteColor)
            {
                case "#f5f5f5":
                    btnTag.SetTextColor(Color.ParseColor("#000000"));
                    break;
                default:
                    btnTag.SetTextColor(Color.ParseColor(INFONOTE.NoteColor.Insert(1, "90")));
                    break;
            }
            btnTag.Gravity = GravityFlags.Center | GravityFlags.Top;




            /*############################################################################################################################*/
            /*############################################################################################################################*/









            /*############################################################################################################################*/
            /*############################################################################################################################*/



            TextView DisplayNoteText = new TextView(this);
            DisplayNoteText.SetX(25);
            DisplayNoteText.SetY(140);
            DisplayNoteText.Typeface = Robot_Regular;
            DisplayNoteText.Text = INFONOTE.NoteText;
            DisplayNoteText.SetMaxLines(2);
            DisplayNoteText.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.MatchParent);
            DisplayNoteText.SetTextSize(Android.Util.ComplexUnitType.Sp, 15);
            DisplayNoteText.SetPadding(0, 0, 40, 0);
            DisplayNoteText.SetTextColor(Color.ParseColor("#4A4A4A"));




            TextView noteId = new TextView(this)
            {
                Visibility = ViewStates.Invisible,
                Text = INFONOTE.NoteID.ToString()
            };



            TextView noteColor = new TextView(this)
            {
                Visibility = ViewStates.Invisible,
                Text = INFONOTE.NoteColor
            };

            TextView notePassword = new TextView(this);
            notePassword.Text = INFONOTE.Password;
            notePassword.Visibility = ViewStates.Invisible;

            TextView noteDate = new TextView(this);
            noteDate.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
            noteDate.SetPadding(0, 30, 30, 0);
            noteDate.Gravity = GravityFlags.End;
            noteDate.Text = INFONOTE.NoteTime;
            noteDate.TextSize = 12;
            noteDate.SetTextColor(Color.Silver);
            noteDate.Visibility = ViewStates.Visible;
            noteDate.SetX(20);
            noteDate.SetY(40);


            TextView noteFullText = new TextView(this);
            noteFullText.Text = INFONOTE.FullText;
            noteFullText.Visibility = ViewStates.Invisible;
            TextView notePassIcon = new TextView(this);
            notePassIcon.Typeface = Segeo;
            notePassIcon.Text = "\n\nPassword is required to read the content";
            notePassIcon.TextSize = 18;
            notePassIcon.SetBackgroundColor(Color.ParseColor("#fafafa"));
            notePassIcon.Gravity = GravityFlags.Center;
            notePassIcon.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);



            TextView noteImp = new TextView(this);
            noteImp.Text = INFONOTE.Important;
            noteImp.Visibility = ViewStates.Invisible;

            TextView noteModified = new TextView(this);
            noteModified.Text = INFONOTE.DateModified;
            noteModified.Visibility = ViewStates.Invisible;


            TextView noteTitle = new TextView(this);
            noteTitle.Text = INFONOTE.NoteTitle;

            noteTitle.SetTextSize(Android.Util.ComplexUnitType.Sp, 20);
            noteTitle.SetTextColor(Color.DarkGray);
            noteTitle.SetTypeface(ArialNB, TypefaceStyle.Bold);
            noteTitle.Visibility = ViewStates.Visible;


            TextView noteDot = new TextView(this);
            noteDot.Text = "●";
            noteDot.SetX(10);
            noteDot.SetY(40);
            noteDot.SetTextSize(Android.Util.ComplexUnitType.Sp, 20);
            noteDot.SetTextColor(Color.ParseColor(INFONOTE.NoteColor.Insert(1, "90")));
            noteDot.SetTypeface(ArialNB, TypefaceStyle.Bold);
            noteDot.Visibility = ViewStates.Visible;


            TextView noteAudioPath = new TextView(this);
            noteAudioPath.SetX(10);
            noteAudioPath.SetY(40);
            noteAudioPath.Text = INFONOTE.AudioPath;
            noteAudioPath.Visibility = ViewStates.Invisible;



            switch (INFONOTE.NoteColor)
            {
                case "#f5f5f5":
                    noteDot.SetTextColor(Color.ParseColor("#90505050"));
                    break;
                default:
                    noteDot.SetTextColor(Color.ParseColor(INFONOTE.NoteColor.Insert(1, "90")));
                    break;
            }

            Button buttonAudio = new Button(this);
            buttonAudio.Typeface = Segeo;

            buttonAudio.SetBackgroundResource(Resource.Drawable.insidebuttons);
            buttonAudio.SetPadding(5, 1, 0, 0);
            buttonAudio.SetCompoundDrawablesWithIntrinsicBounds(_ = GetDrawable(Resource.Drawable.headphone), null, null, null);

            buttonAudio.LayoutParameters = new LinearLayout.LayoutParams(75, 75);

            if (string.IsNullOrWhiteSpace(noteAudioPath.Text))
            {
                buttonAudio.Visibility = ViewStates.Invisible;
            }


            Space space = new Space(this);
            space.LayoutParameters = new LinearLayout.LayoutParams(20, 500);
            LinearLayout linearLayout = new LinearLayout(this);
            linearLayout.Orientation = Orientation.Horizontal;
            linearLayout.SetX(60);
            linearLayout.SetY(67);
            linearLayout.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.MatchParent);
            linearLayout.AddView(noteTitle);
            linearLayout.AddView(space);
            linearLayout.AddView(buttonAudio);




            buttonAudio.Click += (s, args) =>
            {
                Android.Media.MediaPlayer mediaPlayer = new Android.Media.MediaPlayer();

                try
                {


                    if (buttonAudio.Text == "Playing... ")
                    {
                        mediaPlayer.Stop();
                        buttonAudio.SetBackgroundResource(Resource.Drawable.btn_mic_paused);
                        buttonAudio.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(Resource.Drawable.icon_mic_paused), null, null, null);
                        buttonAudio.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, 75);
                        buttonAudio.Text = "Paused";

                    }
                    else if (buttonAudio.Text == "Paused")
                    {
                        mediaPlayer.Start();
                        buttonAudio.Text = "Playing... ";

                        buttonAudio.SetBackgroundResource(Resource.Drawable.active_button);
                        buttonAudio.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(Resource.Drawable.icon_mic_playxml), null, null, null);
                        buttonAudio.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, 75);
                    }
                    else
                    {
                        buttonAudio.Text = "Playing... ";

                        buttonAudio.SetBackgroundResource(Resource.Drawable.active_button);
                        buttonAudio.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(Resource.Drawable.icon_mic_playxml), null, null, null);
                        buttonAudio.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, 75);
                        mediaPlayer.SetDataSource(noteAudioPath.Text);

                        mediaPlayer.Prepare();
                        mediaPlayer.Start();
                    }
                    try
                    {

                        mediaPlayer.Completion += (s, args) =>
                        {

                            buttonAudio.LayoutParameters = new LinearLayout.LayoutParams(75, 75);
                            buttonAudio.Text = null;

                            buttonAudio.SetBackgroundResource(Resource.Drawable.insidebuttons);
                            buttonAudio.SetPadding(5, 1, 0, 0);
                            buttonAudio.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(Resource.Drawable.headphone), null, null, null);
                            buttonAudio.RefreshDrawableState();
                            linearLayout.RemoveView(buttonAudio);
                            linearLayout.AddView(buttonAudio);
                            mediaPlayer.Stop();

                        };
                    }
                    catch (Exception)
                    {
                        buttonAudio.Text = "Failed...";
                        buttonAudio.SetBackgroundColor(Color.ParseColor("#80ff5000"));
                        buttonAudio.SetTextColor(Color.White);

                    }
                }
                catch (Exception ee)
                {
                    Android.Util.Log.Debug("W", ee.Message);

                }
            };





            void onClickNote(object sender, EventArgs e)
            {
                if (!string.IsNullOrWhiteSpace(notePassword.Text))
                {
                    using (Android.Support.V7.App.AlertDialog.Builder PasswordAlertBuilder = new Android.Support.V7.App.AlertDialog.Builder(this))
                    {

                        _ = PasswordAlertBuilder.SetTitle("Password is required!");

                        _ = PasswordAlertBuilder.SetPositiveButton(Resource.String.View, delegate
                        {
                            Android.Support.V7.App.AlertDialog.Builder EnterPasswordbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
                            EnterPasswordbuilder.SetMessage("Enter this note password");
                            EditText editPass = new EditText(this);
                            editPass.InputType = Android.Text.InputTypes.TextVariationPassword;
                            _ = EnterPasswordbuilder.SetView(editPass);
                            _ = EnterPasswordbuilder.SetPositiveButton(Resource.String.Unlock, delegate
                            {
                                if (editPass.Text == notePassword.Text)
                                {
                                    INFONOTE.NoteText = noteFullText.Text;
                                    INFONOTE.NoteID = Convert.ToInt32(noteId.Text);
                                    INFONOTE.NoteColor = noteColor.Text;
                                    INFONOTE.Tag = btnTag.Text;
                                    INFONOTE.Password = notePassword.Text;
                                    INFONOTE.NoteTime = noteDate.Text;
                                    INFONOTE.DateModified = noteModified.Text;
                                    INFONOTE.NoteTitle = noteTitle.Text;
                                    INFONOTE.Important = noteImp.Text;
                                    INFONOTE.AudioPath = noteAudioPath.Text;
                                    StartActivity(typeof(NoteEditorActivity));
                                }
                                else
                                {
                                    Toast.MakeText(this, "Wrong Password", ToastLength.Short).Show();
                                }
                            });
                            EnterPasswordbuilder.Create().Show();

                        });
                        _ = PasswordAlertBuilder.SetNegativeButton(Android.Resource.String.Cancel, delegate
                        {
                            PasswordAlertBuilder.Dispose();
                        }
                        );

                        PasswordAlertBuilder.Create().Show();
                    }
                }
                else
                {
                    INFONOTE.NoteText = noteFullText.Text;
                    INFONOTE.NoteID = Convert.ToInt32(noteId.Text);
                    INFONOTE.NoteColor = noteColor.Text;
                    INFONOTE.Tag = btnTag.Text;
                    INFONOTE.Password = notePassword.Text;
                    INFONOTE.NoteTime = noteDate.Text;
                    INFONOTE.DateModified = noteModified.Text;
                    INFONOTE.Important = noteImp.Text;
                    INFONOTE.NoteTitle = noteTitle.Text;
                    INFONOTE.AudioPath = noteAudioPath.Text;

                    StartActivity(typeof(NoteEditorActivity));
                }
            }




            /*############################################################################################################################*/
            /*############################################################################################################################*/


            void ShowFull(object sender, EventArgs e)
            {

                if (!string.IsNullOrWhiteSpace(notePassword.Text))
                {
                    using (Android.Support.V7.App.AlertDialog.Builder PasswordAlertBuilder = new Android.Support.V7.App.AlertDialog.Builder(this))
                    {

                        _ = PasswordAlertBuilder.SetTitle("Password is required!");

                        _ = PasswordAlertBuilder.SetPositiveButton(Resource.String.Unlock_one_time, delegate
                        {
                            Android.Support.V7.App.AlertDialog.Builder EnterPasswordbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
                            EnterPasswordbuilder.SetMessage("Enter this note password");
                            EditText editPass = new EditText(this);
                            editPass.InputType = Android.Text.InputTypes.TextVariationPassword;
                            _ = EnterPasswordbuilder.SetView(editPass);

                            _ = EnterPasswordbuilder.SetPositiveButton(Resource.String.Unlock, delegate
                            {
                                if (editPass.Text == notePassword.Text)
                                {
                                    using (Android.Support.V7.App.AlertDialog.Builder pp = new Android.Support.V7.App.AlertDialog.Builder(this))
                                    {

                                        _ = pp.SetTitle("More...");
                                        _ = pp.SetMessage($"{noteFullText.Text}");

                                        _ = pp.SetPositiveButton(Android.Resource.String.Copy, delegate
                                        {
                                            ((ClipboardManager)base.GetSystemService(ClipboardService)).PrimaryClip = ClipData.NewPlainText("", noteFullText.Text);
                                            Snackbar.Make(ll, "Copied!", 500).Show();

                                        });
                                        _ = pp.SetNegativeButton(Resource.String.Delete, delegate
                                        {
                                            Android.Support.V7.App.AlertDialog.Builder pp = new Android.Support.V7.App.AlertDialog.Builder(this);
                                            _ = pp.SetTitle("Delete confirmation!\nAre you sure to delete?");
                                            _ = pp.SetMessage($"{noteFullText.Text}");
                                            _ = pp.SetNegativeButton(Android.Resource.String.Yes, delegate
                                            {
                                                int tb = new SQLiteConnection(CONNECTION.DBPath).Table<Note>().Delete(x => x.NoteText == noteFullText.Text);
                                                ll.RemoveView(card);
                                                Snackbar.Make(ll, "Successfully removed", 500).Show();
                                            });
                                            _ = pp.SetPositiveButton(Android.Resource.String.Cancel, delegate
                                            {
                                                pp.Dispose();
                                            });
                                            pp.Create().Show();
                                        });
                                        _ = pp.SetNeutralButton(Android.Resource.String.Cancel, delegate
                                        {
                                            pp.Dispose();
                                        });




                                        pp.Create().Show();
                                    }

                                }
                                else
                                {
                                    Toast.MakeText(this, "Wrong password", ToastLength.Short).Show();
                                }
                            });
                            _ = EnterPasswordbuilder.SetNegativeButton(Resource.String.Unlock_remove_password, delegate
                            {


                                if (editPass.Text == notePassword.Text)
                                {

                                    var db = new SQLiteConnection(CONNECTION.DBPath);
                                    var tb = db.Table<Note>().Delete(x => x.NoteText == noteFullText.Text);
                                    Note note = new Note(Convert.ToInt32(noteId.Text), noteColor.Text, noteFullText.Text, noteDate.Text,
                                                          noteImp.Text, btnTag.Text, string.Empty, noteModified.Text, noteTitle.Text, noteAudioPath.Text);
                                    db.Insert(note);
                                    Snackbar.Make(ll, "Password was removed", 500).Show();
                                }
                                else
                                {
                                    Toast.MakeText(this, "Wrong Password", ToastLength.Short).Show();
                                }

                            });
                            _ = EnterPasswordbuilder.SetNeutralButton(Android.Resource.String.Cancel, delegate
                            {
                                EnterPasswordbuilder.Dispose();
                            });
                            EnterPasswordbuilder.Create().Show();

                        });
                        _ = PasswordAlertBuilder.SetNeutralButton(Android.Resource.String.Cancel, delegate
                        {
                            PasswordAlertBuilder.Dispose();
                        });




                        PasswordAlertBuilder.Create().Show();
                    }


                }
                else
                {






                    using (Android.Support.V7.App.AlertDialog.Builder pp = new Android.Support.V7.App.AlertDialog.Builder(this))
                    {

                        _ = pp.SetTitle("More...");
                        _ = pp.SetMessage($"{noteFullText.Text}");

                        _ = pp.SetPositiveButton(Android.Resource.String.Copy, delegate
                        {
                            ((ClipboardManager)base.GetSystemService(ClipboardService)).PrimaryClip = ClipData.NewPlainText("", noteFullText.Text);
                            Snackbar.Make(ll, "Copied!", 500).Show();

                        });
                        _ = pp.SetNegativeButton(Resource.String.Delete, delegate
                        {
                            Android.Support.V7.App.AlertDialog.Builder pp = new Android.Support.V7.App.AlertDialog.Builder(this);
                            _ = pp.SetTitle("Delete confirmation!\nAre you sure to delete?");
                            _ = pp.SetMessage($"{noteFullText.Text}");
                            _ = pp.SetNegativeButton(Android.Resource.String.Yes, delegate
                            {
                                int tb = new SQLiteConnection(CONNECTION.DBPath).Table<Note>().Delete(x => x.NoteText == noteFullText.Text);
                                ll.RemoveView(card);
                                Snackbar.Make(ll, "Successfully removed", 500).Show();
                            });
                            _ = pp.SetPositiveButton(Android.Resource.String.Cancel, delegate
                            {
                                pp.Dispose();
                            });
                            pp.Create().Show();
                        });
                        _ = pp.SetNeutralButton(Android.Resource.String.Cancel, delegate
                        {
                            pp.Dispose();
                        });




                        pp.Create().Show();
                    }
                }
            }

            void FilterByTag(object sender, EventArgs e)
            {
                ll.RemoveAllViews();
                foreach (Note note in from Note item in new SQLiteConnection(CONNECTION.DBPath).Table<Note>().Where(x => x.Tag == btnTag.Text)
                                      let note = new Note(item.num, item.NoteColor, item.NoteText, item.NoteTime, item.Important, item.Tag, item.Password, item.DateModified, item.NoteTitle, item.AudioPath)
                                      select note)
                {
                    INFONOTE.NoteColor = note.NoteColor;
                    INFONOTE.TagColor = note.NoteColor;
                    INFONOTE.Tag = btnTag.Text;
                    INFONOTE.NoteText = note.NoteText;
                    INFONOTE.NoteTime = note.NoteTime;
                    INFONOTE.Important = note.Important;
                    INFONOTE.Password = note.Password;
                    INFONOTE.DateModified = note.DateModified;
                    INFONOTE.NoteTitle = note.NoteTitle;
                    INFONOTE.AudioPath = note.AudioPath;


                    InitializeControlsForBottomViewNotes();

                }
                FindViewById<ScrollView>(Resource.Id.scrollProfileNotes).ScrollTo(0, 0);
            }


            btnTag.Click += FilterByTag;
            DisplayNoteText.Click += onClickNote;
            DisplayNoteText.LongClick += ShowFull;
            card.Click += onClickNote;
            card.LongClick += ShowFull;


            if (!string.IsNullOrWhiteSpace(notePassword.Text))
            {
                card.AddView(btnTag);
                card.AddView(notePassword);
                card.AddView(noteFullText);
                card.AddView(notePassIcon);
                card.AddView(noteId);
                card.AddView(noteColor);
                card.AddView(noteDate);
                card.AddView(noteImp);
                card.AddView(noteModified);
                card.AddView(noteDot);
                card.AddView(linearLayout);

                ll.AddView(card);
                notePassIcon.BringToFront();
                noteTitle.BringToFront();
                noteDot.BringToFront();
            }
            else
            {
                card.AddView(btnTag);
                card.AddView(notePassword);
                card.AddView(noteFullText);
                card.AddView(noteId);
                card.AddView(noteColor);
                card.AddView(noteDate);
                card.AddView(noteImp);
                card.AddView(DisplayNoteText);
                card.AddView(noteModified);
                card.AddView(noteDot);
                card.AddView(linearLayout);

                ll.AddView(card);

            }
            // BottomBtnsLayout.AddView(btnTag);


            //  ll.AddView(sp1);


        }

    }
}