import { useState } from "react";
import ChatInput from "./ChatInput";
import MessageList from "./MessageList";
import type { ChatMessage } from "./types";
import { streamChat } from "./api";

export default function ChatPage() {

    const [messages, setMessages] = useState<ChatMessage[]>([]);

    const handleSend = async (text: string) => {
        setMessages((prev) => [
            ...prev,
            { role: "user", content: text },
            { role: "assistant", content: "" },
        ]);

        try {
            for await (const chunk of streamChat(text)) {
                setMessages((prev) => {
                    const last = prev[prev.length - 1];
                    const rest = prev.slice(0, -1);

                    return [
                        ...rest,
                        { ...last, content: last.content + chunk },
                    ];
                });
            }
        } catch {
            setMessages((prev) => {
                const last = prev[prev.length - 1];
                const rest = prev.slice(0, -1);

                return [
                    ...rest,
                    { ...last, content: "Nie udało się uzyskać odpowiedzi." },
                ];
            });
        }
    };


    return (
        <div className="mx-auto flex w-full max-w-3xl flex-col gap-6 py-12">
            <MessageList messages={messages} />
            <ChatInput onSend={handleSend} />
        </div>
    )
}
