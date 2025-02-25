EXTERNAL GiveNotes()

VAR QuestState = "REQUIREMENTS_NOT_MET"

=== npc ===
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
        ¡Qué bueno!¿Me podés compartir tus notas?
    *[No]
        A mí tampoco. La explicación apresurada no me permitió tomar anotaciones.
    *[...]
--> END

=inProgress
    ¿Tenes tus notas?
-> END

=canFinish
    ¿Me compartís tus notas?
    ->notes
-> END

=finished
    ¡Gracias por las notas!
-> END

=== notes ===
   *[Si] 
        ~GiveNotes()   
        Gracias 
    *[No]
        :(
--> END