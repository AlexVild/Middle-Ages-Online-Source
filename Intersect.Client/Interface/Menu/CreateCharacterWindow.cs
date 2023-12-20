using System;
using System.Collections.Generic;

using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Game.Chat;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.GameObjects;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Menu
{

    public partial class CreateCharacterWindow
    {

        private Button mBackButton;

        private ImagePanel mCharacterContainer;

        private ImagePanel mCharacterNameBackground;

        private ImagePanel mCharacterPortrait;

        private ImagePanel mDecorSelections;

        private ImagePanel mSkinSelectionContainer;

        private ImagePanel mHairSelectionContainer;

        private ImagePanel mEyesSelectionContainer;

        private ImagePanel mClothingSelectionContainer;

        private ImagePanel mExtrasSelectionContainer;

        private ImagePanel mBeardSelectionContainer;

        //Image
        private string mCharacterPortraitImg = "";

        private Label mCharCreationHeader;

        //Controls
        private ImagePanel mCharCreationPanel;

        private Label mCharnameLabel;

        private TextBox mCharnameTextbox;

        private ImagePanel mClassBackground;

        private ComboBox mClassCombobox;

        private Label mClassLabel;

        private Button mCreateButton;

        private int mDisplaySpriteIndex = -1;

        private LabeledCheckBox mFemaleChk;

        private List<KeyValuePair<int, ClassSprite>> mFemaleSprites = new List<KeyValuePair<int, ClassSprite>>();

        private ImagePanel mGenderBackground;

        private Label mGenderLabel;

        private string[] mSelectedDecors;

        private Label mHint2Label;

        private Label mHintLabel;

        private Label mSkinLabel;

        private Label mHairLabel;

        private Label mEyeLabel;

        private Label mClothesLabel;

        private Label mExtraLabel;

        private Label mBeardLabel;

        private List<string> mAvailableHairs = new List<string>();
        private int mHairIndex = 0;

        private List<string> mAvailableEyes = new List<string>();
        private int mEyeIndex = 0;

        private List<string> mAvailableClothes = new List<string>();
        private int mClothesIndex = 0;

        private List<string> mAvailableExtras = new List<string>();
        private int mExtrasIndex = 0;

        private List<string> mAvailableBeards = new List<string>();
        private int mBeardIndex = 0;

        public ImagePanel[] mPaperdollPanels;

        //Parent
        private MainMenu mMainMenu;

        private LabeledCheckBox mMaleChk;

        //Class Info
        private List<KeyValuePair<int, ClassSprite>> mMaleSprites = new List<KeyValuePair<int, ClassSprite>>();

        private Button mNextSpriteButton;
        private Button mPrevSpriteButton;

        private Button mNextHairButton;
        private Button mPrevHairButton;

        private Button mNextEyesButton;
        private Button mPrevEyesButton;

        private Button mNextClothesButton;
        private Button mPrevClothesButton;

        private Button mNextExtraButton;
        private Button mPrevExtraButton;

        private Button mNextBeardButton;
        private Button mPrevBeardButton;

        private SelectCharacterWindow mSelectCharacterWindow;

        //Init
        public CreateCharacterWindow(
            Canvas parent,
            MainMenu mainMenu,
            ImagePanel parentPanel,
            SelectCharacterWindow selectCharacterWindow
        )
        {
            //Assign References
            mMainMenu = mainMenu;
            mSelectCharacterWindow = selectCharacterWindow;

            //Main Menu Window
            mCharCreationPanel = new ImagePanel(parent, "CharacterCreationWindow");
            mCharCreationPanel.IsHidden = true;

            //Menu Header
            mCharCreationHeader = new Label(mCharCreationPanel, "CharacterCreationHeader");
            mCharCreationHeader.SetText(Strings.CharacterCreation.title);

            //Character name Label
            mCharnameLabel = new Label(mCharCreationPanel, "CharacterNameLabel");
            mCharnameLabel.SetText(Strings.CharacterCreation.name);

            //Character Name Background
            mCharacterNameBackground = new ImagePanel(mCharCreationPanel, "CharacterNamePanel");

            //Character name Textbox
            mCharnameTextbox = new TextBox(mCharacterNameBackground, "CharacterNameField");
            mCharnameTextbox.SubmitPressed += CharnameTextbox_SubmitPressed;
            
            //Class Label
            mClassLabel = new Label(mCharCreationPanel, "ClassLabel");
            mClassLabel.SetText(Strings.CharacterCreation.Class);

            //Class Background
            mClassBackground = new ImagePanel(mCharCreationPanel, "ClassPanel");

            //Class Combobox
            mClassCombobox = new ComboBox(mClassBackground, "ClassCombobox");
            mClassCombobox.ItemSelected += classCombobox_ItemSelected;

            _CreateCharacterWindow();

            //Hint Label
            mHintLabel = new Label(mCharCreationPanel, "HintLabel");
            mHintLabel.SetText(Strings.CharacterCreation.hint);
            mHintLabel.IsHidden = true;

            //Hint2 Label
            mHint2Label = new Label(mCharCreationPanel, "Hint2Label");
            mHint2Label.SetText(Strings.CharacterCreation.hint2);
            mHint2Label.IsHidden = true;

            //Character Container
            mCharacterContainer = new ImagePanel(mCharCreationPanel, "CharacterContainer");

            //Character sprite
            mCharacterPortrait = new ImagePanel(mCharacterContainer, "CharacterPortait");
            mCharacterPortrait.SetSize(48, 48);

            // Decor stuff

            mDecorSelections = new ImagePanel(mCharCreationPanel, "DecorSelectionsContainer");

            mSkinSelectionContainer = new ImagePanel(mDecorSelections, "SkinSelectionContainer");
            mHairSelectionContainer = new ImagePanel(mDecorSelections, "HairSelectionContainer");
            mEyesSelectionContainer = new ImagePanel(mDecorSelections, "EyesSelectionContainer");
            mClothingSelectionContainer = new ImagePanel(mDecorSelections, "ClothingSelectionContainer");
            mExtrasSelectionContainer = new ImagePanel(mDecorSelections, "ExtrasSelectionContainer");
            mBeardSelectionContainer = new ImagePanel(mDecorSelections, "BeardSelectionContainer");

            //Class Background
            mGenderBackground = new ImagePanel(mCharCreationPanel, "GenderPanel");

            //Gender Label
            mGenderLabel = new Label(mGenderBackground, "GenderLabel");
            mGenderLabel.SetText(Strings.CharacterCreation.gender);

            //Skin
            mPrevSpriteButton = new Button(mSkinSelectionContainer, "PreviousSpriteButton");
            mPrevSpriteButton.Clicked += _prevSpriteButton_Clicked;
            mSkinLabel = new Label(mSkinSelectionContainer, "SkinLabel");
            mSkinLabel.SetText(Strings.CharacterCreation.skincolor);
            mNextSpriteButton = new Button(mSkinSelectionContainer, "NextSpriteButton");
            mNextSpriteButton.Clicked += _nextSpriteButton_Clicked;

            //Hair
            mPrevHairButton = new Button(mHairSelectionContainer, "PreviousHairButton");
            mPrevHairButton.Clicked += _prevHairButton_Clicked;
            mHairLabel = new Label(mHairSelectionContainer, "HairLabel");
            mHairLabel.SetText(Strings.CharacterCreation.hair);
            mNextHairButton = new Button(mHairSelectionContainer, "NextHairButton");
            mNextHairButton.Clicked += _nextHairButton_Clicked;

            //Eyes Label
            mPrevEyesButton = new Button(mEyesSelectionContainer, "PreviousEyesButton");
            mPrevEyesButton.Clicked += _prevEyesButton_Clicked;
            mEyeLabel = new Label(mEyesSelectionContainer, "EyeLabel");
            mEyeLabel.SetText(Strings.CharacterCreation.eyes);
            mNextEyesButton = new Button(mEyesSelectionContainer, "NextEyesButton");
            mNextEyesButton.Clicked += _nextEyesButton_Clicked;

            //Clothes Label
            mPrevClothesButton = new Button(mClothingSelectionContainer, "PreviousClothesButton");
            mPrevClothesButton.Clicked += _prevClothesButton_Clicked;
            mClothesLabel = new Label(mClothingSelectionContainer, "ClothesLabel");
            mClothesLabel.SetText(Strings.CharacterCreation.clothes);
            mNextClothesButton = new Button(mClothingSelectionContainer, "NextClothesButton");
            mNextClothesButton.Clicked += _nextClothesButton_Clicked;

            //Extra Label
            mPrevExtraButton = new Button(mExtrasSelectionContainer, "PreviousExtrasButton");
            mPrevExtraButton.Clicked += _prevExtrasButton_Clicked;
            mExtraLabel = new Label(mExtrasSelectionContainer, "ExtraLabel");
            mExtraLabel.SetText(Strings.CharacterCreation.extra);
            mNextExtraButton = new Button(mExtrasSelectionContainer, "NextExtrasButton");
            mNextExtraButton.Clicked += _nextExtrasButton_Clicked;

            //Beard Label
            mPrevBeardButton = new Button(mBeardSelectionContainer, "PreviousBeardButton");
            mPrevBeardButton.Clicked += _prevBeardButton_Clicked;
            mBeardLabel = new Label(mBeardSelectionContainer, "BeardLabel");
            mBeardLabel.SetText(Strings.CharacterCreation.beard);
            mNextBeardButton = new Button(mBeardSelectionContainer, "NextBeardButton");
            mNextBeardButton.Clicked += _nextBeardButton_Clicked;

            //Male Checkbox
            mMaleChk = new LabeledCheckBox(mGenderBackground, "MaleCheckbox")
            {
                Text = Strings.CharacterCreation.male
            };

            mMaleChk.IsChecked = true;
            mMaleChk.Checked += maleChk_Checked;
            mMaleChk.UnChecked += femaleChk_Checked; // If you notice this, feel free to hate us ;)

            //Female Checkbox
            mFemaleChk = new LabeledCheckBox(mGenderBackground, "FemaleCheckbox")
            {
                Text = Strings.CharacterCreation.female
            };

            mFemaleChk.Checked += femaleChk_Checked;
            mFemaleChk.UnChecked += maleChk_Checked;

            //Register - Send Registration Button
            mCreateButton = new Button(mCharCreationPanel, "CreateButton");
            mCreateButton.SetText(Strings.CharacterCreation.create);
            mCreateButton.Clicked += CreateButton_Clicked;

            mBackButton = new Button(mCharCreationPanel, "BackButton");
            mBackButton.IsHidden = true;
            mBackButton.SetText(Strings.CharacterCreation.back);
            mBackButton.Clicked += BackButton_Clicked;

            mCharCreationPanel.LoadJsonUi(GameContentManager.UI.Menu, Graphics.Renderer.GetResolutionString());
        }

        public bool IsHidden => mCharCreationPanel.IsHidden;

        public void Init()
        {
            LoadDecors(ref mAvailableHairs, ref mAvailableEyes, ref mAvailableClothes, ref mAvailableExtras, ref mAvailableBeards, true, true);
            mClassCombobox.DeleteAll();
            var classCount = 0;
            foreach (ClassBase cls in ClassBase.Lookup.Values)
            {
                if (!cls.Locked)
                {
                    mClassCombobox.AddItem(cls.Name);
                    classCount++;
                }
            }
            LoadClass();
            UpdateDisplay();
        }

        private int mTotalSprites => mMaleChk.IsChecked ? mMaleSprites.Count : mFemaleSprites.Count;

        public void Update()
        {
            if (!Networking.Network.Connected)
            {
                Hide();
                mMainMenu.Show();
                Interface.MsgboxErrors.Add(new KeyValuePair<string, string>("", Strings.Errors.lostconnection));

                return;
            }

            // Re-Enable our buttons if we're not waiting for the server anymore with it disabled.
            if (!Globals.WaitingOnServer && mCreateButton.IsDisabled)
            {
                mCreateButton.Enable();
            }

            mSkinLabel.SetText($"{Strings.CharacterCreation.skincolor} ({mDisplaySpriteIndex + 1} / {mTotalSprites})");
            mEyeLabel.SetText($"{Strings.CharacterCreation.eyes} ({mEyeIndex + 1 } / { mAvailableEyes.Count})");
            mExtraLabel.SetText($"{Strings.CharacterCreation.extra} ({mExtrasIndex + 1 } / { mAvailableExtras.Count})");
            mHairLabel.SetText($"{Strings.CharacterCreation.hair} ({mHairIndex + 1 } / { mAvailableHairs.Count})");
            mBeardLabel.SetText($"{Strings.CharacterCreation.beard} ({mBeardIndex + 1} / { mAvailableBeards.Count})");
            mClothesLabel.SetText($"{Strings.CharacterCreation.clothes} ({mClothesIndex + 1} / { mAvailableClothes.Count})");
        }

        //Methods
        private void UpdateDisplay()
        {
            var isFace = true;
            UpdateClassInfoText();
            if (GetClass() != null && mDisplaySpriteIndex != -1)
            {
                mCharacterPortrait.IsHidden = false;
                if (GetClass().Sprites.Count > 0)
                {
                    if (mMaleChk.IsChecked)
                    {
                        /*mCharacterPortrait.Texture = Globals.ContentManager.GetTexture( DO NOT WANT face
                            GameContentManager.TextureType.Face, mMaleSprites[mDisplaySpriteIndex].Value.Face
                        );*/

                        mCharacterPortrait.Texture = null;

                        if (mCharacterPortrait.Texture == null)
                        {
                            mCharacterPortrait.Texture = Globals.ContentManager.GetTexture(
                                GameContentManager.TextureType.Entity, mMaleSprites[mDisplaySpriteIndex].Value.Sprite
                            );

                            isFace = false;
                        }
                    }
                    else
                    {
                        /*mCharacterPortrait.Texture = Globals.ContentManager.GetTexture(
                            GameContentManager.TextureType.Face, mFemaleSprites[mDisplaySpriteIndex].Value.Face
                        );*/

                        mCharacterPortrait.Texture = null;

                        if (mCharacterPortrait.Texture == null)
                        {
                            mCharacterPortrait.Texture = Globals.ContentManager.GetTexture(
                                GameContentManager.TextureType.Entity, mFemaleSprites[mDisplaySpriteIndex].Value.Sprite
                            );

                            isFace = false;
                        }
                    }

                    if (mCharacterPortrait.Texture != null)
                    {
                        if (isFace) // never
                        {
                            mCharacterPortrait.SetTextureRect(
                                0, 0, mCharacterPortrait.Texture.GetWidth(), mCharacterPortrait.Texture.GetHeight()
                            );

                            var scale = Math.Min(
                                mCharacterContainer.InnerWidth / (double)mCharacterPortrait.Texture.GetWidth(),
                                mCharacterContainer.InnerHeight / (double)mCharacterPortrait.Texture.GetHeight()
                            );

                            mCharacterPortrait.SetSize(
                                (int)(mCharacterPortrait.Texture.GetWidth() * scale),
                                (int)(mCharacterPortrait.Texture.GetHeight() * scale)
                            );

                            mCharacterPortrait.SetPosition(
                                mCharacterContainer.Width / 2 - mCharacterPortrait.Width / 2,
                                mCharacterContainer.Height / 2 - mCharacterPortrait.Height / 2
                            );
                        }
                        else
                        {
                            mCharacterPortrait.SetTextureRect(
                                0, 0, mCharacterPortrait.Texture.GetWidth() / Options.Instance.Sprites.NormalFrames,
                                mCharacterPortrait.Texture.GetHeight() / Options.Instance.Sprites.Directions
                            );

                            mCharacterPortrait.SetSize(
                                mCharacterPortrait.Texture.ScaledWidth / Options.Instance.Sprites.NormalFrames, mCharacterPortrait.Texture.ScaledHeight / Options.Instance.Sprites.Directions
                            );

                            mCharacterPortrait.SetPosition(
                                mCharacterContainer.Width / 2 - mCharacterPortrait.Width / 2,
                                mCharacterContainer.Height / 2 - mCharacterPortrait.Height / 2
                            );
                        }
                    }
                    // Draw decors
                    if (mPaperdollPanels == null)
                    {
                        var paperdollSlots = Options.DecorSlots.Count;
                        mPaperdollPanels = new ImagePanel[paperdollSlots + 1];
                        for (var i = 0; i <= paperdollSlots; i++)
                        {
                            mPaperdollPanels[i] = new ImagePanel(mCharacterContainer);
                            mPaperdollPanels[i].Hide();
                        }
                    }
                    

                    for (var z = 0; z < Options.DecorSlots.Count; z++)
                    {
                        string paperdoll = mSelectedDecors[z];

                        if (string.IsNullOrWhiteSpace(paperdoll))
                        {
                            mPaperdollPanels[z].Texture = null;
                            mPaperdollPanels[z].Hide();
                        }
                        else
                        {
                            var paperdollTex = Globals.ContentManager.GetTexture(GameContentManager.TextureType.Decor, paperdoll);

                            mPaperdollPanels[z].Texture = paperdollTex;
                            if (paperdollTex != null)
                            {
                                mPaperdollPanels[z]
                                    .SetTextureRect(
                                        0, 0, mPaperdollPanels[z].Texture.GetWidth() / Options.Instance.Sprites.NormalFrames,
                                        mPaperdollPanels[z].Texture.GetHeight() / Options.Instance.Sprites.Directions
                                    );

                                mPaperdollPanels[z]
                                    .SetSize(
                                        mPaperdollPanels[z].Texture.ScaledWidth / Options.Instance.Sprites.NormalFrames,
                                        mPaperdollPanels[z].Texture.ScaledHeight / Options.Instance.Sprites.Directions
                                    );

                                mPaperdollPanels[z]
                                    .SetPosition(
                                        mCharacterContainer.Width / 2 - mPaperdollPanels[z].Width / 2,
                                        mCharacterContainer.Height / 2 - mPaperdollPanels[z].Height / 2
                                    );
                            }

                            mPaperdollPanels[z].Show();
                        }
                    }
                }
            }
            else
            {
                mCharacterPortrait.IsHidden = true;
            }
        }

        public void Show()
        {
            mCharCreationPanel.Show();
        }

        public void Hide()
        {
            mCharCreationPanel.Hide();
        }

        private ClassBase GetClass()
        {
            if (mClassCombobox.SelectedItem == null)
            {
                return null;
            }

            foreach (var cls in ClassBase.Lookup)
            {
                if (mClassCombobox.SelectedItem.Text == cls.Value.Name && !((ClassBase) cls.Value).Locked)
                {
                    return (ClassBase) cls.Value;
                }
            }

            return null;
        }

        private void LoadClass()
        {
            var cls = GetClass();
            mMaleSprites.Clear();
            mFemaleSprites.Clear();
            mDisplaySpriteIndex = -1;
            if (cls != null)
            {
                for (var i = 0; i < cls.Sprites.Count; i++)
                {
                    if (cls.Sprites[i].Gender == 0)
                    {
                        mMaleSprites.Add(new KeyValuePair<int, ClassSprite>(i, cls.Sprites[i]));
                    }
                    else
                    {
                        mFemaleSprites.Add(new KeyValuePair<int, ClassSprite>(i, cls.Sprites[i]));
                    }
                }
            }

            ResetSprite(false);
        }

        private void ResetSprite(bool clearSelection)
        {
            mNextSpriteButton.IsHidden = true;
            mPrevSpriteButton.IsHidden = true;
            LoadDecors(ref mAvailableHairs, ref mAvailableEyes, ref mAvailableClothes, ref mAvailableExtras, ref mAvailableBeards, mMaleChk.IsChecked, clearSelection);
            if (mMaleChk.IsChecked)
            {
                mBeardSelectionContainer.Show();
                if (mMaleSprites.Count > 0)
                {
                    mDisplaySpriteIndex = 0;
                    if (mMaleSprites.Count > 1)
                    {
                        mNextSpriteButton.IsHidden = false;
                        mPrevSpriteButton.IsHidden = false;
                    }
                }
                else
                {
                    mDisplaySpriteIndex = -1;
                }
            }
            else
            {
                mBeardSelectionContainer.Hide();
                if (mFemaleSprites.Count > 0)
                {
                    mDisplaySpriteIndex = 0;
                    if (mFemaleSprites.Count > 1)
                    {
                        mNextSpriteButton.IsHidden = false;
                        mPrevSpriteButton.IsHidden = false;
                    }
                }
                else
                {
                    mDisplaySpriteIndex = -1;
                }
            }
        }

        private void _prevSpriteButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            mDisplaySpriteIndex--;
            if (mMaleChk.IsChecked)
            {
                if (mMaleSprites.Count > 0)
                {
                    if (mDisplaySpriteIndex == -1)
                    {
                        mDisplaySpriteIndex = mMaleSprites.Count - 1;
                    }
                }
                else
                {
                    mDisplaySpriteIndex = -1;
                }
            }
            else
            {
                if (mFemaleSprites.Count > 0)
                {
                    if (mDisplaySpriteIndex == -1)
                    {
                        mDisplaySpriteIndex = mFemaleSprites.Count - 1;
                    }
                }
                else
                {
                    mDisplaySpriteIndex = -1;
                }
            }

            UpdateDisplay();
        }

        private void _nextSpriteButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            mDisplaySpriteIndex++;
            if (mMaleChk.IsChecked)
            {
                if (mMaleSprites.Count > 0)
                {
                    if (mDisplaySpriteIndex >= mMaleSprites.Count)
                    {
                        mDisplaySpriteIndex = 0;
                    }
                }
                else
                {
                    mDisplaySpriteIndex = -1;
                }
            }
            else
            {
                if (mFemaleSprites.Count > 0)
                {
                    if (mDisplaySpriteIndex >= mFemaleSprites.Count)
                    {
                        mDisplaySpriteIndex = 0;
                    }
                }
                else
                {
                    mDisplaySpriteIndex = -1;
                }
            }

            UpdateDisplay();
        }

        private void _nextHairButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            mHairIndex = safelyIterateDecorIndex(mHairIndex + 1, mAvailableHairs);
            mSelectedDecors[Options.DecorSlots.IndexOf("Hair")] = mAvailableHairs[mHairIndex];

            UpdateDisplay();
        }

        private void _prevHairButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            mHairIndex = safelyIterateDecorIndex(mHairIndex - 1, mAvailableHairs);
            mSelectedDecors[Options.DecorSlots.IndexOf("Hair")] = mAvailableHairs[mHairIndex];

            UpdateDisplay();
        }

        private void _nextEyesButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            mEyeIndex = safelyIterateDecorIndex(mEyeIndex + 1, mAvailableEyes);
            mSelectedDecors[Options.DecorSlots.IndexOf("Eyes")] = mAvailableEyes[mEyeIndex];

            UpdateDisplay();
        }

        private void _prevEyesButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            mEyeIndex = safelyIterateDecorIndex(mEyeIndex - 1, mAvailableEyes);
            mSelectedDecors[Options.DecorSlots.IndexOf("Eyes")] = mAvailableEyes[mEyeIndex];

            UpdateDisplay();
        }

        private void _nextClothesButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            mClothesIndex = safelyIterateDecorIndex(mClothesIndex + 1, mAvailableClothes);
            mSelectedDecors[Options.DecorSlots.IndexOf("Shirt")] = mAvailableClothes[mClothesIndex];

            UpdateDisplay();
        }

        private void _prevClothesButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            mClothesIndex = safelyIterateDecorIndex(mClothesIndex -1, mAvailableClothes);
            mSelectedDecors[Options.DecorSlots.IndexOf("Shirt")] = mAvailableClothes[mClothesIndex];

            UpdateDisplay();
        }

        private void _nextExtrasButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            mExtrasIndex = safelyIterateDecorIndex(mExtrasIndex + 1, mAvailableExtras);
            mSelectedDecors[Options.DecorSlots.IndexOf("Extra")] = mAvailableExtras[mExtrasIndex];

            UpdateDisplay();
        }

        private void _prevExtrasButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            mExtrasIndex = safelyIterateDecorIndex(mExtrasIndex - 1, mAvailableExtras);
            mSelectedDecors[Options.DecorSlots.IndexOf("Extra")] = mAvailableExtras[mExtrasIndex];

            UpdateDisplay();
        }

        private void _nextBeardButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            mBeardIndex = safelyIterateDecorIndex(mBeardIndex + 1, mAvailableBeards);
            mSelectedDecors[Options.DecorSlots.IndexOf("Beard")] = mAvailableBeards[mBeardIndex];

            UpdateDisplay();
        }

        private void _prevBeardButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            mBeardIndex = safelyIterateDecorIndex(mBeardIndex - 1, mAvailableBeards);
            mSelectedDecors[Options.DecorSlots.IndexOf("Beard")] = mAvailableBeards[mBeardIndex];

            UpdateDisplay();
        }

        int safelyIterateDecorIndex (int newIdx, List<string> collection)
        {
            if (newIdx >= collection.Count)
            {
                newIdx = 0;
            } else if (newIdx < 0)
            {
                newIdx = collection.Count - 1;
            }

            return newIdx;
        }

        void TryCreateCharacter(int gender)
        {
            if (Globals.WaitingOnServer || mDisplaySpriteIndex == -1)
            {
                return;
            }

            if (FieldChecking.IsValidUsername(mCharnameTextbox.Text, Strings.Regex.username))
            {
                FadeService.FadeOut(callback: () =>
                {
                    if (mMaleChk.IsChecked)
                    {
                        PacketSender.SendCreateCharacter(
                            mCharnameTextbox.Text, GetClass().Id, mMaleSprites[mDisplaySpriteIndex].Key, mSelectedDecors
                        );
                    }
                    else
                    {
                        PacketSender.SendCreateCharacter(
                            mCharnameTextbox.Text, GetClass().Id, mFemaleSprites[mDisplaySpriteIndex].Key, mSelectedDecors
                        );
                    }

                    Globals.WaitingOnServer = true;
                    mCreateButton.Disable();
                    ChatboxMsg.ClearMessages();
                });
            }
            else
            {
                Interface.MsgboxErrors.Add(new KeyValuePair<string, string>("", Strings.CharacterCreation.invalidname));
            }
        }

        //Input Handlers
        void CharnameTextbox_SubmitPressed(Base sender, EventArgs arguments)
        {
            if (mMaleChk.IsChecked == true)
            {
                TryCreateCharacter(0);
            }
            else
            {
                TryCreateCharacter(1);
            }
        }

        void classCombobox_ItemSelected(Base control, ItemSelectedEventArgs args)
        {
            LoadClass();
            UpdateDisplay();
        }

        void maleChk_Checked(Base sender, EventArgs arguments)
        {
            mMaleChk.IsChecked = true;
            mFemaleChk.IsChecked = false;
            ResetSprite(true);
            UpdateDisplay();
        }

        void femaleChk_Checked(Base sender, EventArgs arguments)
        {
            mFemaleChk.IsChecked = true;
            mMaleChk.IsChecked = false;
            ResetSprite(true);
            UpdateDisplay();
        }

        void CreateButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            if (Globals.WaitingOnServer)
            {
                return;
            }

            if (mMaleChk.IsChecked == true)
            {
                TryCreateCharacter(0);
            }
            else
            {
                TryCreateCharacter(1);
            }
        }

        private void BackButton_Clicked(Base sender, ClickedEventArgs arguments)
        {
            Hide();
            if (Options.Player.MaxCharacters <= 1)
            {
                //Logout
                mMainMenu.Show();
            }
            else
            {
                //Character Selection Screen
                mSelectCharacterWindow.Show();
            }
        }

        private void ClearDecors(bool clearSelection)
        {
            mAvailableHairs.Clear();
            mAvailableHairs.Add("");
            mAvailableEyes.Clear();
            mAvailableEyes.Add("");
            mAvailableClothes.Clear();
            mAvailableExtras.Clear();
            mAvailableExtras.Add("");
            mAvailableBeards.Clear();
            mAvailableBeards.Add("");
            if (clearSelection)
            {
                mHairIndex = 0;
                mEyeIndex = 0;
                mClothesIndex = 0;
                mExtrasIndex = 0;
                mBeardIndex = 0;
                
                mSelectedDecors = new string[Options.DecorSlots.Count];
            }
        }

        private void LoadDecors(ref List<string> hairs, ref List<string> eyes, ref List<string> clothes, ref List<string> extras, ref List<string> beards, bool isMale, bool clearSelection)
        {
            ClearDecors(clearSelection);

            // Used to determine when to load a texture
            var genderString = "m";
            if (!isMale) genderString = "f";

            Func<string, bool> isGender = genderPrefix =>
            {
                return genderPrefix.Equals(genderString) || genderPrefix.Equals("u");
            };

            var decorTextures = Globals.ContentManager.GetTextureNames(GameContentManager.TextureType.Decor);

            foreach (var decorTexture in decorTextures)
            {
                var splitFileName = decorTexture.Split("_".ToCharArray());
                if (splitFileName.Length >= 3) // prefix_gender_name
                {
                    var prefix = splitFileName[0].ToLower();
                    var genderPrefix = splitFileName[1].ToLower();

                    foreach (string decorSlot in Options.DecorSlots)
                    {
                        var lowerDecorSlot = decorSlot.ToLower();
                        if (prefix.Equals(lowerDecorSlot))
                        {
                            switch (lowerDecorSlot)
                            {
                                case "hair":
                                    if (isGender(genderPrefix))
                                    {
                                        hairs.Add(decorTexture);
                                    }
                                    break;
                                case "eyes":
                                    if (isGender(genderPrefix))
                                    {
                                        eyes.Add(decorTexture);
                                    }
                                    break;
                                case "shirt":
                                    if (isGender(genderPrefix))
                                    {
                                        clothes.Add(decorTexture);
                                    }
                                    break;
                                case "extra":
                                    if (isGender(genderPrefix))
                                    {
                                        extras.Add(decorTexture);
                                    }
                                    break;
                                case "beard":
                                    if (isGender(genderPrefix))
                                    {
                                        beards.Add(decorTexture);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            // Sort clothing so that default is first, as these always need selected
            clothes.Sort(new DefaultComparer());
            // Set defaults
            if (clearSelection)
            {
                mSelectedDecors[(int)Options.Instance.PlayerOpts.ShirtSlot] = clothes[0];
            }
        }

    }

    class DefaultComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x.Contains("default"))
            {
                return -1;
            }
            if (y.Contains("default"))
            {
                return 1;
            }

            return 0;
        }
    }

    public partial class CreateCharacterWindow
    {
        private ScrollControl ClassInfoContainer { get; set; }

        private RichLabel ClassInfoText { get; set; }

        private Label ClassInfoTemplate { get; set; }

        void _CreateCharacterWindow()
        {
            ClassInfoContainer = new ScrollControl(mCharCreationPanel, "ClassInfoContainer");
            ClassInfoTemplate = new Label(ClassInfoContainer, "ClassInfoTemplate");
            ClassInfoText = new RichLabel(ClassInfoContainer);

            ClassInfoContainer.ScrollToTop();
        }

        private void UpdateClassInfoText()
        {
            var cls = GetClass();
            string text;
            switch(cls.Name.ToLower())
            {
                case "mage":
                    text = Strings.CharacterCreation.ClassInfoMage;
                    break;
                case "rogue":
                    text = Strings.CharacterCreation.ClassInfoRogue;
                    break;
                case "warrior":
                    text = Strings.CharacterCreation.ClassInfoWarrior;
                    break;
                default:
                    text = Strings.CharacterCreation.ClassInfoMage;
                    break;
            }

            ClassInfoText.ClearText();
            ClassInfoText.Width = ClassInfoContainer.Width -
                ClassInfoContainer.GetVerticalScrollBar().Width;
            ClassInfoText.AddText(text, ClassInfoTemplate);
            ClassInfoText.SizeToChildren(false, true);
            ClassInfoContainer.ScrollToTop();
        }
    }
}
