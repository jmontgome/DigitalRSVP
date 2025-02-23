export function IsNullOrWhitespace(string: string): boolean {
    return (!string || string.trim() == "");
}