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
-> END

=canStart
-> END

=inProgress
-> END

=canFinish
-> END

=finished
-> END

¡Hola!
¿Te gustó la clase de hoy?
*[Si]
    ¡Qué bueno! Me compartís tus notas.
    ->notes
*[No]
    A mí tampoco. La explicación apresurada no me permitió tomar anotaciones.
*[...]
--> END

=== notes ===
   *[Si] 
        ~GiveNotes()   
        Gracias 
    *[No]
        :(
--> END