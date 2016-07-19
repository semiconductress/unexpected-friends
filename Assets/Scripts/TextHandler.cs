using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TextHandler {

	string[] testtext1 = { "Hi, I'm tall.", "How are you?" };
	Flag[] flagsnull = {};
	string[] choices1 = { "Good :D", "Bad :(" };
	Flag[][] setflags1 = {new Flag[] {new Flag("ImGood",true)},new Flag[] {new Flag("ImGood",false)}};

	string[] testtext2 = { "That's cool!" };
	Flag[] flags2 = {new Flag("ImGood",true)};
	string[] choicesnull = {};
	Flag[][] setnull = {};

	string[] testtext3 = { "That sucks." };
	Flag[] flags3 = {new Flag("ImGood",false)};

	string[] testtext4 = { "Hi, I'm short.", "Go away.", "Please." };
	Flag[] flags4 = {};

	string[] testtext5 = { "Hi, I'm short.", "But I heard you're feeling down, we can talk.", "You really feeling bad, huh?" };
	Flag[] flags5 = {new Flag("ImGood",false)};
	string[] choices5 = { "Yeah.", "Nah." };
	Flag[][] setflags5 = {new Flag[] {new Flag("ImGood2",false)},new Flag[] {new Flag("ImGood2",true)}};

	string[] testtext6 = { "Same." };
	Flag[] flags6 = {new Flag("ImGood",false), new Flag("ImGood2",false)};

	string[] testtext7 = { "Don't lie to us." };
	Flag[] flags7 = {new Flag("ImGood",false), new Flag("ImGood2",true)};

	string[] testtext8 = { "Why would you lie? I really cared about you.", "I'm rethinking our friendship." };
	Flag[] flags8 = {new Flag("ImGood",false), new Flag("ImGood2",true)};
	string[] choices8 = { "Sorry.", "Meh." };
	Flag[][] setflags8 = {new Flag[] {new Flag("ImSorry",true)},new Flag[] {new Flag("ImSorry",false)}};

	string[] testtext9 = { "You should be sorry." };
	Flag[] flags9 = {new Flag("ImGood",false), new Flag("ImGood2",true), new Flag("ImSorry",true)};

	string[] testtext10 = { "What an ass." };
	Flag[] flags10 = {new Flag("ImGood",false), new Flag("ImGood2",true), new Flag("ImSorry",false)};

	string[] testtext11 = { "You're alright for apologizing, I guess." };
	Flag[] flags11 = {new Flag("ImGood",false), new Flag("ImGood2",true), new Flag("ImSorry",true)};

	string[] testtext12 = { "God, you're the worst." };
	Flag[] flags12 = {new Flag("ImGood",false), new Flag("ImGood2",true), new Flag("ImSorry",false)};

	string[] testtext13 = { "Get off me!" };
	Flag[] flags13 = {new Flag("StandingOn",true)};

	string[] testtext14 = { "Oh, hello!" };
	Flag[] flags14 = {new Flag("StandingOn",true)};

	string[] testtext15 = { "Get off me, liar." };
	Flag[] flags15 = {new Flag("StandingOn",true), new Flag("ImGood2",true)};

	string[] testtext16 = { "It's me, Short Head, again.", "How did I get up here? Dunno.", "Who were you expecting, Ben Stiller?" };
	Flag[] flags16 = {new Flag ("REQNOT:Clifftalked", true)};
	string[] choices16 = { "Why male models?", "Teddy Roosevelt." };
	Flag[][] setflags16 = {new Flag[] {new Flag("Zoolander",true)},new Flag[] {new Flag("Zoolander",false)}};

	string[] testtext17 = { "Great movie.", "Anyway." };
	Flag[] flags17 = {new Flag ("Zoolander",true), new Flag ("REQNOT:Clifftalked",true)};
	Flag[][] setflags17 = {new Flag[] {new Flag("Clifftalked",true)}};

	string[] testtext18 = { "Not his best work.", "Anyway." };
	Flag[] flags18 = {new Flag ("Zoolander",false), new Flag ("REQNOT:Clifftalked",true)};
	Flag[][] setflags18 = {new Flag[] {new Flag("Clifftalked",true)}};

	ArrayList tallhead = new ArrayList();
	ArrayList shorthead = new ArrayList();
	ArrayList shortheadcliff = new ArrayList();

	public Dictionary<string, ArrayList> text = new Dictionary<string, ArrayList>();
	public ArrayList globalflags;
	public Text dialoguebox;

	public bool inchoice;
	public bool canstart;
	public string currentname;
	public string printname;
	public DialogueSegment currentseg;
	public int currenttextindex;

	//Testing

	public TextHandler (ArrayList f, Text t) {
		tallhead.Add (new DialogueSegment (testtext1, flagsnull, choices1, setflags1, DialogueSegment.CONT));
		tallhead.Add (new DialogueSegment (testtext2, flags2, choicesnull, setnull, DialogueSegment.EXIT));
		tallhead.Add (new DialogueSegment (testtext3, flags3, choicesnull, setnull, DialogueSegment.EXIT));
		tallhead.Add (new DialogueSegment (testtext8, flags8, choices8, setflags8, DialogueSegment.CONT));
		tallhead.Add (new DialogueSegment (testtext9, flags9, choicesnull, setnull, DialogueSegment.EXIT));
		tallhead.Add (new DialogueSegment (testtext10, flags10, choicesnull, setnull, DialogueSegment.EXIT));
		tallhead.Add (new DialogueSegment (testtext14, flags14, choicesnull, setnull, DialogueSegment.EXIT));
		tallhead.Add (new DialogueSegment (testtext15, flags15, choicesnull, setnull, DialogueSegment.EXIT));

		shorthead.Add (new DialogueSegment (testtext4, flagsnull, choicesnull, setnull, DialogueSegment.EXIT));
		shorthead.Add (new DialogueSegment (testtext5, flags5, choices5, setflags5, DialogueSegment.CONT));
		shorthead.Add (new DialogueSegment (testtext6, flags6, choicesnull, setnull, DialogueSegment.EXIT));
		shorthead.Add (new DialogueSegment (testtext7, flags7, choicesnull, setnull, DialogueSegment.EXIT));
		shorthead.Add (new DialogueSegment (testtext11, flags11, choicesnull, setnull, DialogueSegment.EXIT));
		shorthead.Add (new DialogueSegment (testtext12, flags12, choicesnull, setnull, DialogueSegment.EXIT));
		shorthead.Add (new DialogueSegment (testtext13, flags13, choicesnull, setnull, DialogueSegment.EXIT));
		shorthead.Add (new DialogueSegment (testtext15, flags15, choicesnull, setnull, DialogueSegment.EXIT));

		shortheadcliff.Add (new DialogueSegment (testtext16, flags16, choices16, setflags16, DialogueSegment.CONT));
		shortheadcliff.Add (new DialogueSegment (testtext17, flags17, choicesnull, setflags17, DialogueSegment.CONT));
		shortheadcliff.Add (new DialogueSegment (testtext18, flags18, choicesnull, setflags18, DialogueSegment.CONT));
		foreach (DialogueSegment seg in shorthead) {
			shortheadcliff.Add (seg);
		}

		text.Add ("Tall Head", tallhead);
		text.Add ("Short Head", shorthead);
		text.Add ("Short Head:oncliff", shortheadcliff);

		inchoice = false;
		canstart = true;
		globalflags = f;
		dialoguebox = t;
		currenttextindex = 0;
		currentname = "";
		printname = "";
		currentseg = null;
	}

	public string StartConvo (string name) {
		Debug.Log (name);
		currentseg = MostSharedFlags (globalflags, text [name]);
		currentname = name;
		printname = name.Split(':')[0];
		currenttextindex = 0;
		dialoguebox.text = printname + ": " + currentseg.text [0];
		dialoguebox.enabled = true;
		return (printname + ": " + currentseg.text[0]);
	}

	public string StartConvo (string name, Flag flag) {
		currentseg = MostSharedFlags (globalflags, text [name], flag);
		currentname = name;
		printname = name.Split(':')[0];
		currenttextindex = 0;
		dialoguebox.text = printname + ": " + currentseg.text [0];
		dialoguebox.enabled = true;
		return (printname + ": " + currentseg.text[0]);
	}

	public string AdvanceConvo (float choiceinput) {
		if (inchoice && choiceinput != 0) {
			if (choiceinput < 0) {
				SetFlags (currentseg.setflags [0]);
			} else if (choiceinput > 0) {
				SetFlags (currentseg.setflags [1]);
			}
			inchoice = false;
			return EndConvo ();
		} else if (inchoice) {
			return null;
		} else {
			currenttextindex++;
			if (currenttextindex == currentseg.text.Length - 1) {
				if (currentseg.choices.Length > 1) {
					inchoice = true;
					dialoguebox.text = printname + ": " + currentseg.text [currenttextindex] +
					"\n\n" + "A: " + currentseg.choices [0] + "    S: " + currentseg.choices [1];
					return printname + ": " + currentseg.text [currenttextindex] +
					"\n\n" + "A: " + currentseg.choices [0] + "    S: " + currentseg.choices [1];
				}
			} else if (currenttextindex == currentseg.text.Length) {
				if (currentseg.setflags.Length == 1) {
					SetFlags (currentseg.setflags [0]);
				}
				return EndConvo ();
			}
			dialoguebox.text = printname + ": " + currentseg.text [currenttextindex];
			return printname + ": " + currentseg.text [currenttextindex];
		}
	}

	public string EndConvo () {
		inchoice = false;
		currenttextindex = 0;
		if (currentseg.endtype.Equals(DialogueSegment.CONT)) {
			return StartConvo (currentname);
		} else if (currentseg.endtype.Equals(DialogueSegment.EXIT)) {
			dialoguebox.text = "";
			dialoguebox.enabled = false;
			currentseg = null;
			currentname = "";
			return "END";
		}
		return "END";
	}

	public void ResetConvo () {
		inchoice = false;
		currenttextindex = 0;
		currentseg = null;
		currentname = "";
		dialoguebox.text = "";
		dialoguebox.enabled = false;
	}

	public bool SetFlag (Flag flag) {
		foreach (Flag gflag in globalflags) {
			if (gflag.name == flag.name) {
				Flag.SetValue (gflag, flag.value);
				return true;
			}
		}
		globalflags.Add (flag);
		return false;
	}

	public void SetFlags (Flag[] flags) {
		foreach (Flag flag in flags) {
			SetFlag (flag);
		}
	}

	public static int SharedFlags (ArrayList allflags, Flag[] testflags) {
		int i = 0;
		foreach (Flag testflag in testflags) {
			//Debug.Log (testflag.name + " " + testflag.name.Contains("NOT"));
			if (testflag.name.Contains ("REQ")) {
				if (testflag.name.Contains("NOT")) {
					if (!allflags.Contains (new Flag(testflag.name.Split(':')[1], testflag.value))) {
						Debug.Log (testflag.name.Split(':')[1] + " " + allflags.Contains (new Flag(testflag.name.Split(':')[1], testflag.value)));
						i+= 10000;
					} else {
						return -1;
					}
				} else if (allflags.Contains (testflag)) {
					i += 10000;
				} else {
					return -1;
				}
			} else if (testflag.name.Contains("NOT")) {
				if (!allflags.Contains (new Flag(testflag.name.Split(':')[1], testflag.value))) {
					i++;
				} else {
					return -1;
				}
			} else if (allflags.Contains (testflag)) {
				i++;
			} else {
				return -1;
			}
		}
		Debug.Log (i);
		return i;
	}

	public static int SharedFlags (ArrayList allflags, Flag[] testflags, Flag require) {
		if (testflags.Length > 0)
			Debug.Log (testflags[0].name);
		if (!testflags.Contains (require)) {
			return -1;
		}
		return SharedFlags (Flag.TempAddFlag(allflags,require), testflags);
	}

	public static int SharedFlags (ArrayList allflags, Flag[] testflags, Flag require, bool b) {
		if (testflags.Length > 0)
			Debug.Log (testflags[0].name);
		if (testflags.Contains (require) && !b) {
			return -1;
		}
		return SharedFlags (Flag.TempAddFlag(allflags,require), testflags);
	}

	public static DialogueSegment MostSharedFlags (ArrayList allflags, ArrayList segs) {
		DialogueSegment mostseg = null;
		int max = -1;
		foreach (DialogueSegment seg in segs) {
			int shared = SharedFlags (allflags, seg.flags);
			if (shared > max) {
				max = shared;
				mostseg = seg;
			}
		}
		return mostseg;
	}

	public static DialogueSegment MostSharedFlags (ArrayList allflags, ArrayList segs, Flag require) {
		DialogueSegment mostseg = null;
		int max = -1;
		foreach (DialogueSegment seg in segs) {
			int shared = SharedFlags (allflags, seg.flags, require);
			if (shared > max) {
				max = shared;
				mostseg = seg;
			}
		}
		return mostseg;
	}

	public static DialogueSegment MostSharedFlags (ArrayList allflags, ArrayList segs, Flag require, bool b) {
		DialogueSegment mostseg = null;
		int max = -1;
		foreach (DialogueSegment seg in segs) {
			int shared = SharedFlags (allflags, seg.flags, require, b);
			if (shared > max) {
				max = shared;
				mostseg = seg;
			}
		}
		return mostseg;
	}

}

public class DialogueSegment {
	public string[] text;
	public Flag[] flags;
	public string[] choices; //0-3 values only!
	public Flag[][] setflags;
	public string endtype; //use the following endtypes

	public const string EXIT = "EXIT";
	public const string CONT = "CONT";

	public DialogueSegment(string[] t, Flag[] f, string[] c, Flag[][] s, string end){
		text = t;
		flags = f;
		choices = c;
		setflags = s;
		endtype = end;
	}

}

public class Flag {

	public string name;
	public bool value;

	public Flag(string n, bool b) {
		name = n;
		value = b;
	}

	public static void SetValue(Flag f, bool b) {
		f.value = b;
	}

	public static ArrayList TempAddFlag(ArrayList gfs, Flag f) {
		ArrayList tempflags = new ArrayList ();
		foreach (Flag gf in gfs) {
			tempflags.Add (gf);
		}
		tempflags.Add (f);
		return tempflags;
	}

	public override bool Equals(System.Object obj) {
		if (obj == null)
			return false;

		Flag f = obj as Flag;
		if ((System.Object)f == null)
			return false;
		
		return (name.Equals(f.name) && value == f.value);
	}

	public bool Equals(Flag f) {
		if ((object)f == null)
			return false;
		
		return (name.Equals(f.name) && value == f.value);
	}
}