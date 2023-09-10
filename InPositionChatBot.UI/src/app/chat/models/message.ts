export interface Message {
    id: string;
    sender: Sender;
    text: string;
    date: Date;
}

export interface UIMessage extends Message{
    state: State;
}

export enum State{
    success = "Success",
    error = "Error",
    noResponse = "NoResponse"
}

export enum Sender{
    AI,
    User
}