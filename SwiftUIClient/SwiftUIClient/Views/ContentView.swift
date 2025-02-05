//
//  ContentView.swift
//  SwiftUIClient
//
//  Created by Sistracia on 10/02/24.
//

import SwiftUI
import SignalRClient

struct ContentView: View {
    @State private var connection = HubConnectionBuilder(url: URL(string: "http://localhost:5050/chat")!)
        .withLogging(minLogLevel: .error)
        .build()
    @State private var messages: [Message] = []
    
    var body: some View {
        ChatList(messages: messages, sendMessage: { message in
            connection.send(method: "send", "SwiftUI", message)
        })
        .task {
            connection.on(method: "broadcastMessage", callback: { (sender: String, content: String) in
                messages.append(Message(sender: sender, content: content))
            })
            connection.start()
        }
    }
}

#Preview {
    ContentView()
}
