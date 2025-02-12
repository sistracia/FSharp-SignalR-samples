//
//  ChatRow.swift
//  SwiftUIClient
//
//  Created by Sistracia on 11/02/24.
//

import SwiftUI

struct ChatRow: View {
    let message: Message
    var body: some View {
        HStack {
            Text(message.sender)
            Text(":")
            Text(message.content)
        }
        .frame(
            maxWidth: .infinity,
            alignment: .leading
        )
        .padding(.all, 10)
        .cornerRadius(5)
        .overlay(
            RoundedRectangle(cornerRadius: 5)
                .stroke(.gray)
        )
    }
}

#Preview {
    ChatRow(message: Message.sampleMessages[0])
}
