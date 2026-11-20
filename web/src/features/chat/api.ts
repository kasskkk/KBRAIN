export async function* streamChat(question: string): AsyncGenerator<string> {

    const response = await fetch("/api/chat/stream", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ question }),
    });

    if (!response.ok || !response.body) {
        throw new Error(`Błąd API: ${response.status}`);
    }

    const reader = response.body.getReader();
    const decoder = new TextDecoder();
    let buffer = "";

    while (true) {
        const { done, value } = await reader.read();
        if (done) break;

        buffer += decoder.decode(value, { stream: true });

        const events = buffer.split("\n\n");
        buffer = events.pop() ?? "";

        for (const event of events) {
            const dataLines = event
                .split("\n")
                .filter((line) => line.startsWith("data:"))
                .map((line) => (line.startsWith("data: ") ? line.slice(6) : line.slice(5)));

            if (dataLines.length === 0) {
                continue;
            }

            yield dataLines.join("\n");
        }
    }
}