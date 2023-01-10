INCLUDE globals.ink
{   
    -elliot_quest_completed == true: -> quest_finished
    -else:
    {elliot_quest_started == true: -> already_started | ->scene_1}
    
}

 
=== scene_1 ===
Hello, little one I am glad to see you are alive.  #speaker:Moose #portrait:Moose #layout:Right #speed:30
Are you alright otherwise?
The storm seems to have washed you downstream, it is a wonder you are alive.#speaker:Moose #portrait:Moose #layout:Right #speed:30
Hi, no I seem fine. . . just wet and tired. Where am I?  #speaker:Sylvester #portrait:Sylvester #layout:Left #speed:30
A tiny isle in the stream I enjoy visiting. Sadly it seems to have been hit by the storm too. The lightning caused a fire. :(    #speaker:Moose #portrait:Moose #layout:Right #speed:30
I am sorry to hear that and I wish I could help. But I need to get back home. #speaker:Sylvester #portrait:Sylvester #layout:Left #speed:30
I believe we can help one another. You help me to replant some of the lost plants and I will give you a ride across the water. #speaker:Moose #portrait:Moose #layout:Right #speed:30
~ elliot_quest_started = true
-> END

=== already_started ===
Did you find and plant the berries?.#speaker:Moose #portrait:Moose #layout:Right #speed:30
No I'm still working on it.  #speaker:Sylvester #portrait:Sylvester #layout:Left #speed:30
Ok. I'll talk to you when its done.#speaker:Moose #portrait:Moose #layout:Right #speed:30
->END

=== quest_finished ===
Congratulations!! You Did it.#speaker:Moose #portrait:Moose #layout:Right #speed:30
Lets Move Onto the next task.#speaker:Moose #portrait:Moose #layout:Right #speed:30
~ elliot_quest_started = false
~ elliot_quest_completed = false
~ elliot = elliot + 1
->END