using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Player player;

    public QuestGiver questGiver;

    public Dialogue dialogue0, dialogueQuestActive, dialogueQuestFound, dialogue1, dialogue2, dialogue3;
    private void Start()
    {
        if(player == null)
        {
            player = FindObjectOfType<Player>();
        }
        if(questGiver == null)
        {
            questGiver = FindObjectOfType<QuestGiver>();
        }
    }
    //Paladin
    //d1
    //I'm so tired, i have finally become one of the guardians of this land but all I do is just find things that are lost.
    //Is this what it means to become the saviour of the people of this land !? T-to ...to find LOST ITEMS  !!?
    //Oh sorry ! (i hope that person didn't hear it) I didn't notice you,  I said nothing haha, how may i help you..?
    /*
     * (You) Nothing I was just . . . passing by, but you seem a little bit troubled.
     * (You) This is a fine day, so you should have a good time as well. Guardians always lend a hand to others without a question. So this time, I am willing to help you as well if you have any troubles.
     * No. . .well, actually today morning a lady asked me to find a old book that she had dropped somewhere while on her way from home to some distant land.
     * Such a old lady should not get out from the home into the woods in the first place,let alone take the book with her. But seems like the book was that much important to her to make her keep it with her always.
     * The main trouble is that she doesn't remember the path she took as she is so old. 
     * How can someone in their sane mind look for a letter all over this huge forest !! But I don't want to dissapoint her as well.
     * So, i was hoping if you have some time off help me look for the book. Considering her age , who knows if she actually dropped it in woods or somewhere else.
     * Looking for it all over the woods will take too long. Try asking someone in the village first. 
     */
    /*d2
     * (You) Have you heard of the rumors about the legendary sword Excalibur recently?
     * (You) Im actually on a quest to find it. Any clues if you know can help.I know about as much as an ordinary person that when you go near the middle of woods mysterious lights begin to shine ...and what seems from a distance is that a sword is the source of that light
     * But anyone who has ever approached it ....the light fades away.
     * d3
     * Are you in any trouble. Say me if any help is needed 
     * (You) Oh...( I can't just go around talking about treasures with everyone..) 
Nothing , Im fine of course. And I hope you are doing good too !
     * Surely , Im ...
*waves sword
    */
    /*Donquit
     * d0
     * Yes? You need something?
     * My shop has got the finest weapons and armor. Hmm... Blades, helmets. Pretty much anything to suit your needs.
     * Looking to protect yourself !? or deal some damage?
     * (You) No. I am just here to witness your hard work . .
     * Make it quick incase you want something from me. It annoys me if you keep staring at my face.
     * d1
     * (You) Got some time old man. It feels like a fine day with you around
     * Why is that ?
     * (You) Keeping an eye on the people of your village is reassuring afterall.
     * No need to butter me up. Also you better be more carefull with me.
     * (You) Im looking for some information on a old lady carrying a book, she went out of village ... Probably 3-4 days back.
     * (You) Not sure if you can remember, but will be a big help if you do.
     * Now that you say it, a old lady some days before did approach my shop. She said she needs a fire torch to travel at night i told her to fetch it from inside and then she left. 
     * Thats about all i know, young man.
     * (You) Thats fine, i will take a look inside myslef.
     * d2
     * Ah ... no matter how much i try... It seems my collection will remain unfullfilled.
     * (You) Which collection are you talking about ... are you day dreaming ?
     * You won't understand ... its like a dream for me...my collection. I collect swords that enchants my eyes and i also collect swords that cuts any metal.
     * But I have a unique space for the swords that belong to both of those catagory.
The swords in my shop you see right? ....not all of them are for sell , hahaha
     * Some of them belong to my personal collection. Im just lucky enough to find those treasures and not fool enough to sell those.
     * Bleak Star, Azure Dream, Finisher, Cosmic Ascension, Highroler, Finality, Orcbane, God's Will ....only one of their kind, cannot be replicated are some of these swords. But the one that will complete my collection...
     * .....The Excalibur
I am even willing to pay all of my wealth i have earned for it !
     * (You) Have to tried to find and acquire it??
     * I looked all over. My body is so strong even at this age. But there is only so much that i can acomplish. I can't look for it after days and days of failure to find it.
     * (You) Have you heard the rumors of a bright sowrd appearing only at night !?
     * Yes, I know, they say the Excalibur is lying there waiting to be picked , lit by moon light, under a foggy sky. They say such presence can only be of Excalibur. But I heard its only visible from a distance. As anyone approaches the light fades out and no one is able to find it.
     * I heard its somewhere in the middle of the woods where the stars can be seen and fireflies cover the sky. Perhaps a pond ? But as you said.. only appears at night !
     * (You) I think i can do it. People say the sword choose who it wants to be weid by. Maybe one of the people can find it. And we won't know unless everyone tries.
     * Ha ! Ofcouse you can, I like the young bloods. Taking a task that hasn't been acomplished by any. But I know , I feel...you can. If you really though, or if anyone does it, i have put up a poster to reward a huge sum of money for them.
     * If you the one Excalibur chooses you have all the right to weild it, but when you don't ....my collection is where it shall rest.
     * (You) I will.. make sure your collection is complete . (waves)
     * d3
     * (You) ....
( Never, blacksmits are the least to trust when it comes to treasures )
     * What might you need young man.
     * (You) Nothting.. nothing !!
     * ..... okay
     */
    /*Megan
     * d0
     * Its so good to see you again !
     * (You) You never move from here Megan !
     * Yes, I like this place very much . . . Just the perfect place to enjoy time away from my parents who quarrel all the time.
     * (You) Oh-h.. I thought things had gotten better for you..
     * Not really friend, things don't get better. Just ... We just have to learn to deal with them in a better way.
     * If I try to stop them, it just increases more, So this is where i feel most peacefull.
     * d1
     * (You) Hey there , You fine today ?
     * Yes I'm good. In fact I was wondering if you can join our dinner tonight. Dad and Mom will be happy if you come over.
     * (You) Ah..sorry for that, maybe some other time.
     * (You) Actually looking for some information on a old lady. Speaking of which you sit here usually, have you seen someone like that ? maybe a day befor or two??
     * Sigh..guess you got no luck. I haven't seen any nor do i attempt to see. Staring at skies is all I do.
     * d2
     * (You) Megan are you free tonight, i want some help looking for the legendary sword Excallibur.
     * (You) There is a prize money for it , maybe you can use it. 
     * Sounds good to me...But have you got any clue about it.
     * (You) Well.....I have heard some rumours ...thats about it
     * Trust me we won't get anywhere with just. Thats about what just everyone knows....sounds like it will take a lot of walk too....
     * But...maybe i can help in some other aspect... I actually over heard some men talking about it.
     * They were talking that its only appears at night. And...even more....
     * I heard they said its somewhere in the middle of the woods where the stars can be seen and fireflies cover the sky. Perhaps a .....umm ...not sure ... But as I said.. only appears at night !
Im sure this will help you a lot.
     * But apart from that, ...I don't think it will be something i can do with you. I will just slow you down....
     * (You) Thats okay ! We can just go slow in that case. We will have a good time i think 
     * Its hard to say no when someone says it that way...but still I have no way to cover so much distance i think...so just be reasonable and i have faith you can find it if you are destined to.
     * Thats what the lore says afterall.
     * d3
     * (You) Ehh..Are you okay !?...
Why so gloomy?
     * Oh..you are here...nevermind me....
     * (You) Come on, Im your good friend. Spare me the modest behavior. Tell me whats bothering you.
     * I actually remembered about my treassure box. The one I used to tell you about.. in my childhood.
     * (You) Oh yes I remember, the one where you put all the things you don't want others to find out about....and the things you never want to loose.
     * Yes !,....but the thing is..i hid it somewhere back then. I want to retrieve it now ....BUT I CAN'T REMEMBER ! where did I even hide it !?
     * I know that I burried it somewhere in the woods. But I can't seem to remember where, it even had a treasure. 
I want to find it.
     * (You) It had a TREASSURE !?
     * Why are you getting so excited suddenly ?
     * (You) Oh nothing..i thought knowing more about it can help us find it faster so... yeah...
     * (*stares...)
     * (You)....Sorry, Didn't mean to...
     * Its ok
But...I want to find it, and yes it does have a treasure.
     * (You) You do realise that I atleast need some hints to start with...Right?
     * Well...The Ninjas are good at finding stuffs. Aren't they? 
Maybe try asking one of them.
     * (You) Well I do know a Ninja that I can count on I guess... I will ask him for some advice..hmmmm.... surely I will find him in the rocky valley where he trains usually.
     * Good luck.
     */
    /*Ninja
     * d0
     * .......
     * (You) A ninja !?
     * No a Ninja cosplayer..
     * (You) But you move and ... certainly have the vibe of a ninja.
     * Because I am
     * Trying to perfect my abilities, this is the perfect place to train and try my kunai throws
     * (You) Thats good but seems to me as if you have been doing this training for a long time...
     * Consistency is the key. I don't slack.
     * Now if you will let me ...
     * (You) let you ??
     * Let me ..focus !
     * d1
     * (You) huff..huff..
     * Whats the matter. You seem a bit out of breath.
     * (You) yeah ... been looking for a lost book with no luck. Don't have any clues where to find as well. 
     * (You) Unless ..if you have seen a old lady around somewhere while you train.
     * No.. i focus on my breath, body and posture. But i can sense a human around here with my eyes closed. The trees have ears connected with mine and they can see as well.
     * I don't think the lady you looking for has passed anywhere near here.
     * (You) Guess that reduces the area to look for atleast. Thanks
     * d2
     * (You) Wo...Uhm.. you must have heard about Excalibur ?
     * Im a katana user. However, to someone who is a fighter. Thats a holy blessing. 
     * (You) Do you know how does someone acquire it?
     * I have heard about the rumors but......simply.... i don't trust them. 
I believe the sword chooses the weilder not the person chooses the sword.
     * d3
     * (You) Cymax.., Im here for your advice
     * Ah... Want to learn from the best ?.. I see
     * Yep say what advice of mine would you like.
     * (You) Well.. I want you to teach me how to find hidden items faster.
     * Huh? Theres no advice for that.
     * (You) But isn't it that Ninjas are good at hiding and finding hidden stuffs in the blink of an eye.
     * Thats sounds like what a 8 yr old kid would fantasize about Ninjas. You are hearing too much stories my friend.
     * (You) ........uhh
     * But yes Ninjas are good at it ..no advice but theres a trick
     * Basically Ninjas move really fast so they can look for something fastert than a normal person like you. That and they also priortize places to look for based on what they are looking for.
     * These and some other tricks make them faster. I can't just reveal everything.
     * Tell me what you are looking for. Explain all the information,... only then can I actually help you.
     * (You) Im looking for ..(I can't let him know its some treasure) something important to my friend which she hid in a box and burried somewhere in the woods.
     * If thats the case, you should start looking for it from near where your friend lives and then slowly work your way away from that palce while looking all places... Also where do you think is the best place for someone to hide something important underground ?
     * (You) Somewhere..where no one expects to dig at ?
     * Exactly... perhaps under a house or under a tree or under water maybe !? 
     * (You) Woh... that really bring down the number of places I have to inspect at.
     * Another ninja trick for you my friend..Be sure not to spread the word to masses ...(in slow breath) when you focus hard enough with the picture of what you are looking for in mind, that object will then come to life and call for you.
     * (You) What ? ( what is he saying , what am i supposed to do with that trick...) Is it a riddle, I don't quite understand.
     * Not for me to explain. Move away now, you are eating up my schedule. 
     * (You) Relax ! relax ! ....fine 
I think i should start implementing your golden advice.     (But what was that riddle all about...the box will call me to itelf when i look for it ..? How ?...)
     */
    //------------------old trigger where paladin gave quest only---------
    /*public void TriggerDialogue()
    {
        if (!questGiver.player.quest.isActive && dialogue0.name != "Paladin")
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue0);
        }
        else if(dialogue0.name == "Paladin" && questGiver.player.quest.isActive)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueWhenQuestActivePaladin);
        }
        else
        {
            switch (questGiver.questCount)
            {
                case 1:
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue1);
                    break;
                case 2:
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue2);
                    break;
                default:
                    Debug.LogWarning("Invalid quest count = " + questGiver.questCount);
                    break;
            }
        }
        gameObject.SetActive(false);
    }
    */
    //-------------new trigger where different npc give quests -------------------
    public void TriggerDialogue() {
        if (!questGiver.player.quest.isActive)
        {
            if (dialogue0.name == "Paladin" && questGiver.questCount == 1)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue1);
            }
            else if (dialogue0.name == "Donquit" && questGiver.questCount == 2)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue2);
            }
            else if (dialogue0.name == "Megan" && questGiver.questCount == 3)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue3);
            }
            else
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue0);
            }
        }
        else
        {
            if (dialogue0.name == "Paladin" && questGiver.questCount == 1)
            {
                if (player.quest.goal.targetFound) 
                { 
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogueQuestFound);
                    player.quest.goal.targetSubmitted = true; 
                }
                else { FindObjectOfType<DialogueManager>().StartDialogue(dialogueQuestActive); }
            }
            else if (dialogue0.name == "Donquit" && questGiver.questCount == 2)
            {
                if (player.quest.goal.targetFound) 
                {
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogueQuestFound);
                    player.quest.goal.targetSubmitted = true;
                }
                else { FindObjectOfType<DialogueManager>().StartDialogue(dialogueQuestActive); }
            }
            else if (dialogue0.name == "Megan" && questGiver.questCount == 3)
            {
                if(player.quest.goal.targetFound) 
                { 
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogueQuestFound);
                    player.quest.goal.targetSubmitted = true;
                }
                else { FindObjectOfType<DialogueManager>().StartDialogue(dialogueQuestActive); }
            }
            else
            {
                switch (questGiver.questCount)
                {
                    case 1:
                        FindObjectOfType<DialogueManager>().StartDialogue(dialogue1);
                        break;
                    case 2:
                        FindObjectOfType<DialogueManager>().StartDialogue(dialogue2);
                        break;
                    case 3:
                        FindObjectOfType<DialogueManager>().StartDialogue(dialogue3);
                        break;
                    default:
                        Debug.LogWarning("Invalid quest count = " + questGiver.questCount);
                        break;
                }
            }
        }
        gameObject.SetActive(false);
    }
    
}
