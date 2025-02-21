using System;

public class InputEvents
{
    public InputEventContext inputEventContext { get; private set; } = InputEventContext.DEFAULT;

    public event Action<InputEventContext> onSubmitPressed;
    
    
    public void ChangeInputEventContext(InputEventContext newContext)
    {
        inputEventContext = newContext;
    }

    public void SubmitPressed()
    {
        onSubmitPressed?.Invoke(inputEventContext);
    }
}
