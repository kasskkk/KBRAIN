import { useState } from "react";
import ChatInput from "./ChatInput";
import MessageList from "./MessageList";
import type { ChatMessage } from "./types";

export default function ChatPage() {

    const [messages, setMessages] = useState<ChatMessage[]>([]);

    const handleSend = (text: string) => {
        setMessages((prev) => [
            ...prev,
            { role: "user", content: text }
        ])
    }


    return (
        <div className="mx-auto flex w-full max-w-sm flex-col gap-6 py-12">
            <MessageList messages={messages} />
            <ChatInput onSend={handleSend} />
        </div>
    )
}
