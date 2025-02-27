=== akane === 
#speaker:Akane #portrait:akane_neutral

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
    ¡Hola!
    ¿Te gustó la clase de hoy?
    *[Si]
        ¡Qué bueno!¿Pudiste tomar notas? #portrait:akane_quest
        ->quest
    *[No]
        A mí tampoco. La explicación apresurada no me permitió tomar anotaciones.
    *[...]
--> END

=inProgress
    ¿Tenes tus notas? #portrait:akane_wait
-> END

=canFinish
    ¡Ya tienes tus notas! #portrait:akane_quest
    Creo que Alice está en el Gym
-> END

=finished
    ¡Gracias por prestarle las notas a mi amiga! #portrait:akane_thanks
-> END


=== quest ===
   *[Si]
        ~AcceptQuest()
        ¿Creo que mi amiga necesita las notas?
    *[No]
        :(
--> END