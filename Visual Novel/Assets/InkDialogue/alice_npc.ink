=== alice ===
{QuestState:
    -"FINISHED": -> finished
    -else: -> default
}

=finished
    ¡<color=\#023020>Gracias</color> por las notas! #speaker:Alice #portrait:alice_happy
-> END

=default
Hola, soy la amiga de Akane #speaker:Alice #portrait:alice_neutral
*[...]
    ->END
*{QuestState == "CAN_FINISH"}[¡Te presto mis <b><color=\#00008B>notas!</color></b>]
    ~GiveNotes() 
    ¿Oh? <b><color=\#AA336A>Akane</color></b> te contó... Gracias! #portrait:alice_happy
->END