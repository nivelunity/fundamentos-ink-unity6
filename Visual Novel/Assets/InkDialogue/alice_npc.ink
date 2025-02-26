=== alice ===
{QuestState:
    -"FINISHED": -> finished
    -else: -> default
}

=finished
    ¡Gracias por las notas!
-> END

=default
Hola, soy la amiga de Akane
*[...]
    ->END
*{QuestState == "CAN_FINISH"}[¡Te presto mis notas!]
    ~GiveNotes() 
    ¿Oh? Akane te contó... Gracias!
->END