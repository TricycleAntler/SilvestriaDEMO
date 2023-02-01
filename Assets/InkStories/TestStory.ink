INCLUDE globals.ink
{   
    -elliot_quest_completed == true: -> quest_finished
    -else:
    {elliot_quest_started == true: -> already_started | ->scene_1}
    
}

 
=== scene_1 ===
Hello, little one. I am glad to see you are alive. #layout:Right #speaker:Elliot #portrait:Moose  #speed:30

Are you alright? #speaker:Elliot #portrait:Moose #layout:Right #speed:30

Hi. I seem fine... just tired and wet. #layout:Left #speaker:Silvester #portrait:Silvester #layout:Left #speed:30

The storm seems to have washed you downstream, it is a wonder you are alive.#speaker:Elliot #portrait:Moose #layout:Right #speed:30

Where am I?  #layout:Left #speaker:Silvester #portrait:Silvester #layout:Left #speed:30

A tiny isle in the stream I enjoy visiting. #layout:Right #speaker:Elliot #portrait:Moose  #speed:30

Sadly it seems to have been hit by the storm too.  #layout:Right #speaker:Elliot #portrait:Moose  #speed:30

The lightning caused a fire. :(  #layout:Right  #speaker:Elliot #portrait:Moose  #speed:30

I am sorry to hear that... #layout:Left #speaker:Silvester #portrait:Silvester  #speed:30

I wish I could help, but I need to find a new home.  #layout:Left #speaker:Silvester #portrait:Silvester  #speed:30

I believe we can help one another. #layout:Right #speaker:Elliot #portrait:Moose  #speed:30

There should be some berries lying around here somewhere. #layout:Right #speaker:Elliot #portrait:Moose  #speed:30

If you collect them and plant them in the burnt area over there, I'll help you across the river! #layout:Right #speaker:Elliot #portrait:Moose  #speed:30
~ elliot_quest_started = true
-> END

=== already_started ===
Have you found and planted the berries? #layout:Right #speaker:Elliot #portrait:Moose  #speed:30
No, not yet.  #layout:Left #speaker:Silvester #portrait:Silvester  #speed:30
Okay, get back to me when you're done. #layout:Right #speaker:Elliot #portrait:Moose  #speed:30
->END

=== quest_finished ===
Thank you so much! #layout:Right #speaker:Elliot #portrait:Elliot  #speed:30
Now let's get you across that river. #layout:Right #speaker:Elliot #portrait:Moose #speed:30
~ elliot_quest_started = false
~ elliot_quest_completed = false
~ elliot = elliot + 1
->END