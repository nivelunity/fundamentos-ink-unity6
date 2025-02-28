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
        ¡Qué bueno!¿Pudiste tomar <b><color=\#FF1E35>notas</color></b>? #portrait:akane_happy
        ->quest
    *[No]
        A mí tampoco. La explicación apresurada no me permitió tomar anotaciones. #portrait:akane_sad
    *[...]
--> END

=inProgress
    ¿Tenes tus <b><color=\#FF1E35>notas</color></b>? #portrait:akane_wait
-> END

=canFinish
    ¡Ya tienes tus notas! #portrait:akane_quest
    Creo que Alice está en el Gym
-> END

=finished
    ¡<color=\#F8FF30>Gracias</color> por prestarle las notas a mi amiga! #portrait:akane_thanks
-> END


=== quest ===
   *[Si]
        ~AcceptQuest()
        ¿Creo que mi <color=\#FFFF00>amiga</color> necesita las notas? #portrait:akane_quest
    *[No]
        Qué lástima! #portrait:akane_sad
--> END