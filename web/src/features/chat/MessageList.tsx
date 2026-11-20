import { Bubble, BubbleContent } from "@/components/ui/bubble";
import { Message, MessageContent } from "@/components/ui/message";
import type { ChatMessage } from "./types";
import Markdown from 'react-markdown'
import remarkGfm from 'remark-gfm'

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
                                <BubbleContent>
                                    {isUser ? (
                                        <span className="whitespace-pre-wrap">{message.content}</span>
                                    ) : (
                                        <div className="prose prose-sm dark:prose-invert max-w-none">
                                            <Markdown remarkPlugins={[remarkGfm]}>{message.content}</Markdown>
                                        </div>
                                    )}
                                </BubbleContent>
                            </Bubble>
                        </MessageContent>
                    </Message>
                )
            })}
        </>
    )
}
