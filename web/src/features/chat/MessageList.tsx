import { Bubble, BubbleContent } from "@/components/ui/bubble";
import { Message, MessageContent } from "@/components/ui/message";
import type { ChatMessage } from "./types";

type MessageListProps = {
    messages: ChatMessage[]
}

export default function MessageList({ messages }: MessageListProps) {
    return (
        <>
            {messages.map((message, index) => {
                const isUser = message.role === 'user';

                return (
                    <Message key={index} align={isUser ? "end" : "start"}>
                        <MessageContent>
                            <Bubble variant={isUser ? undefined : "muted"}>
                                <BubbleContent>{message.content}</BubbleContent>
                            </Bubble>
                        </MessageContent>
                    </Message>
                )
            })}
        </>
    )
}
