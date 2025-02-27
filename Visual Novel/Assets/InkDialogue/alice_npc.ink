=== alice ===
{QuestState:
    -"FINISHED": -> finished
    -else: -> default
}

=finished
    ¡Gracias por las notas! #speaker:Alice #portrait:alice_happy
-> END

=default
Hola, soy la amiga de Akane #speaker:Alice #portrait:alice_neutral
*[...]
    ->END
*{QuestState == "CAN_FINISH"}[¡Te presto mis notas!]
    ~GiveNotes() 
    ¿Oh? Akane te contó... Gracias! #portrait:alice_happy
->END