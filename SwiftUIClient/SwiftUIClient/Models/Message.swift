//
//  Message.swift
//  SwiftUIClient
//
//  Created by Sistracia on 10/02/24.
//

import Foundation

struct Message: Identifiable {
    var id: UUID
    var sender: String
    var content: String
    
    init(id: UUID = UUID(), sender: String, content: String) {
        self.id = id
        self.sender = sender
        self.content = content
    }
}


extension Message {
    static let sampleMessages: [Message] = Array(repeating: Message(sender: "Sender", content: "Content"), count: 1000)
}
