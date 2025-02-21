EXTERNAL GiveNotes()

=== npc ===
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