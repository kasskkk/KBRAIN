import { Button } from "@/components/ui/button";
import { Textarea } from "@/components/ui/textarea";
import { type KeyboardEvent, useState } from "react";

type ChatInputProps = {
    onSend: (text: string) => void;
}

export default function ChatInput({ onSend }: ChatInputProps) {

    const [text, setText] = useState("");

    const handleSend = () => {
        const trimmed = text.trim();

        if (!trimmed) return;

        onSend(trimmed);
        setText("");
    };

    const handleKeyDown = (event: KeyboardEvent<HTMLTextAreaElement>) => {
        if (event.key === "Enter" && !event.shiftKey) {
            event.preventDefault();
            handleSend();
        }
    };

    return (
        <>
            <Textarea
                value={text}
                onChange={(e) => setText(e.target.value)}
                onKeyDown={handleKeyDown}
            />
            <Button onClick={handleSend}>Wyślij</Button>
        </>
    )
}
