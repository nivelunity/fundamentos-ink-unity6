=== akane === 
{QuestState:
    -"REQUIREMENTS_NOT_MET": -> requirementsNotMet
    -"CAN_START": -> canStart
    -"IN_PROGRESS": -> inProgress
    -"CAN_FINISH": -> canFinish
    -"FINISHED": -> finished
    -else: -> END
}

=requirementsNotMet
    Creo que no fuimos a la misma clase…
-> END

=canStart
    ¡Hola! #speaker:Akane #portrait:akane_neutral
    ¿Te gustó la clase de hoy?
    *[Si]
        ¡Qué bueno!¿Pudiste tomar <b><color=\#00008B>notas</color></b>? #portrait:akane_happy
        ->quest
    *[No]
        A mí tampoco. La explicación apresurada no me permitió tomar anotaciones. #portrait:akane_sad
    *[...]
--> END

=inProgress
    ¿Tenes tus <b><color=\#00008B>notas</color></b>? #portrait:akane_wait
-> END

=canFinish
    ¡Ya tienes tus notas! #portrait:akane_quest
    Creo que <b><color=\#FF8C00>Alice</color></b> está en el <b><color=\#8b0000>Gym</color></b>
-> END

=finished
    ¡<color=\#023020>Gracias</color> por prestarle las notas a mi amiga! #portrait:akane_thanks
-> END


=== quest ===
   *[Si]
        ~AcceptQuest()
        ¿Creo que mi <b><color=\#FF8C00>amiga</color></b> necesita las notas? #portrait:akane_quest
    *[No]
        Qué lástima! #portrait:akane_sad
--> END