# Architecture Decision Log

Each entry records one decision: its context, the choice made,
rejected alternatives and consequences. Entries are never edited
after acceptance; a changed decision gets a new entry and the old
one is marked as superseded.

**Status values**
- **Proposed**: under consideration, not yet accepted
- **Accepted**: currently in effect
- **Superseded by [date: title]**: replaced by a newer decision
- **Deprecated**: no longer in effect, not replaced

---

## 2026-11-04: Qwen 3.5 9B as the starting model, Bielik for comparison
- **Status:** Accepted
- **Context:** RTX 3060 Ti (8 GB VRAM), answers must be in Polish.
- **Decision:** qwen3.5:9b via Ollama; compare against Bielik later.
- **Why:** Fits in VRAM, good quality-to-speed ratio, supports tools.
- **Consequences:** Run the eval set on both models before choosing.

## 2026-11-06: Model thinking disabled by default
- **Status:** Accepted
- **Context:** With thinking enabled, Qwen 3.5 took 8–40 s to answer even
  simple questions (e.g. "What year is it?"), because it always generates
  a reasoning trace regardless of question difficulty.
- **Decision:** Thinking is disabled by default (`Think: false` in config).
  It will be enabled for the Analyst mode via a separate chat client.
- **Alternatives:** Automatically classifying question difficulty. Rejected
  for the MVP: it needs an extra model call and may misclassify.
- **Consequences:** Answers arrive in ~300 ms. For complex questions
  the user has to select the Analyst mode explicitly.
