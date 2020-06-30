using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Back_Communication;


namespace Back_Server
{
	public static class Database
	{
		// The actual Database
		public static List<Profile> profiles;
		public static List<Message> messages;
		public static List<Topic> topics;
		public static List<Chat> chats;


		// Some useful methods
		private static string path = @"Database XML Files";
		private static string pathProfiles { get { return path + "/Profiles"; } }
		private static string pathMessages { get { return path + "/Messages"; } }
		private static string pathTopics { get { return path + "/Topics"; } }
		private static string pathChats { get { return path + "/Chats"; } }


		/// <summary>
		/// Fills the Databases variables by reading the appropriate XML files
		/// </summary>
		public static void FillDatabase()
		{
			// Initializing the variables
			profiles = new List<Profile>();
			messages = new List<Message>();
			topics = new List<Topic>();
			chats = new List<Chat>();

			// Filling the lists
			PROFILE.ReadAll();
			MESSAGE.ReadAll();
			TOPIC.ReadAll();
			CHAT.ReadAll();
		}


		public static class PROFILE
		{
			/// <summary>
			/// Reads all the XML files containing Profiles
			/// </summary>
			public static void ReadAll()
			{
				DirectoryInfo d = new DirectoryInfo(pathProfiles);
				FileInfo[] files = d.GetFiles("*.xml");

				// Iterating through all the xml Files
				foreach (FileInfo file in files)
				{
					// Reading the Profile
					Profile newProfile = Read(pathProfiles + "/" + file.Name);

					// If not Null, add it to the Database
					if (newProfile != null)
					{
						profiles.Add(newProfile);
					}
				}
			}


			/// <summary>
			/// Reads a given XML file containing a Profile, and adds it to the database
			/// </summary>
			/// <param name="file">Path of the file to read</param>
			private static Profile Read(string file)
			{
				// Creating a new default Profile
				Profile newProfile = new Profile();

				// Opening the XML file
				XmlReader xmlReader = XmlReader.Create(file);

				// Iterating throught the XML file
				while (xmlReader.Read())
				{
					// Filling the fields
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						switch (xmlReader.Name)
						{
							case "ID":
								newProfile.ID = Guid.Parse(xmlReader.ReadElementContentAsString());
								break;

							case "name":
								newProfile.name = xmlReader.ReadElementContentAsString();
								break;

							case "password":
								newProfile.password = xmlReader.ReadElementContentAsString();
								break;

							case "email":
								newProfile.email = xmlReader.ReadElementContentAsString();
								break;

							case "dateCreation":
								newProfile.dateCreation = new DateTime(
									int.Parse(xmlReader.GetAttribute("year")),
									int.Parse(xmlReader.GetAttribute("month")),
									int.Parse(xmlReader.GetAttribute("day")),
									int.Parse(xmlReader.GetAttribute("hour")),
									int.Parse(xmlReader.GetAttribute("minute")),
									int.Parse(xmlReader.GetAttribute("second"))
									);
								break;
						}
					}
				}

				// We finish by closing the xmlReader
				xmlReader.Close();

				// Ensuring we have completed fields
				if (newProfile.ID != Guid.Empty && newProfile.name != "" && newProfile.password != "" && newProfile.email != "")
				{
					// returning the new Profile
					return newProfile;
				}

				// otherwise, return null
				return null;
			}


			/// <summary>
			/// Writes a Profile structure into an XML file
			/// </summary>
			/// <param name="profile">Profile to transcribe into an XML file</param>
			public static void Write(Profile profile)
			{
				XmlTextWriter xmlWriter = new XmlTextWriter(pathProfiles + "/" + profile.ID + ".xml", null);

				// Use automatic indentation for readability
				xmlWriter.Formatting = Formatting.Indented;

				// Write the root element
				xmlWriter.WriteStartElement("profile");

				// ID
				xmlWriter.WriteElementString("ID", profile.ID.ToString());

				// name
				xmlWriter.WriteElementString("name", profile.name);

				// password
				xmlWriter.WriteElementString("password", profile.password);

				// email
				xmlWriter.WriteElementString("email", profile.email);

				// dateCreation
				xmlWriter.WriteStartElement("dateCreation");
				xmlWriter.WriteAttributeString("year", profile.dateCreation.Year.ToString());
				xmlWriter.WriteAttributeString("month", profile.dateCreation.Month.ToString());
				xmlWriter.WriteAttributeString("day", profile.dateCreation.Day.ToString());
				xmlWriter.WriteAttributeString("hour", profile.dateCreation.Hour.ToString());
				xmlWriter.WriteAttributeString("minute", profile.dateCreation.Minute.ToString());
				xmlWriter.WriteAttributeString("second", profile.dateCreation.Second.ToString());
				xmlWriter.WriteEndElement();

				// end the root element
				xmlWriter.WriteFullEndElement();

				//Write the XML to file and close the writer
				xmlWriter.Close();
			}


			/// <summary>
			/// Returns whether or not the Database contains a Profile given its name
			/// </summary>
			/// <param name="name"> Name of the Profile </param>
			/// <returns> Whether or not the Database contains a Profile with this name </returns>
			public static bool ContainsByName(string name)
			{
				// We iterate through all the profiles
				foreach (Profile profile in profiles)
				{
					// If we find a similar profile in the Database
					if (profile.name == name)
					{
						return true;
					}
				}

				// otherwise, we return false
				return false;
			}


			/// <summary>
			/// Returns whether or not the Database contains a Profile given its ID
			/// </summary>
			/// <param name="ID"> ID of the Profile </param>
			/// <returns> Whether or not the Database contains a Profile with this ID </returns>
			public static bool ContainsByID(Guid ID)
			{
				// We iterate through all the profiles
				foreach (Profile profile in profiles)
				{
					// If we find a similar profile in the Database
					if (profile.ID == ID)
					{
						return true;
					}
				}

				// otherwise, we return false
				return false;
			}


			/// <summary>
			/// Returns a Profile of which we know the ID
			/// </summary>
			/// <param name="ID_given"> ID of the Profile we seek </param>
			/// <returns> The instance of the Profile if found, otherwise null </returns>
			public static Profile GetByID(Guid ID_given)
			{
				foreach (Profile profile in profiles)
				{
					if (profile.ID == ID_given)
					{
						return profile;
					}
				}
				return new Profile();
			}


			/// <summary>
			/// Returns a Profile of which we know the name
			/// </summary>
			/// <param name="name"> Name of the Profile we seek </param>
			/// <returns> The instance of the Profile if found, otherwise null </returns>
			public static Profile GetByName(string name)
			{
				// We iterate through all the profiles
				foreach (Profile profile in profiles)
				{
					// If we find a similar profile in the Database
					if (profile.name == name)
					{
						return profile;
					}
				}

				// otherwise, we return null
				return new Profile();
			}
		}


		public static class STRUCTURE
		{
			/// <summary>
			/// Returns a precise Structure from the Database
			/// </summary>
			/// <param name="structureReceived">Structure f which we want to retreive the Database's instance</param>
			/// <returns>The Structure from the Database, or null if not found</returns>
			public static Structure GetByReference(Structure structureReceived)
			{
				// If the structure received is a Topic
				if (structureReceived is Topic)
				{
					// iterate through the Topics
					return Database.TOPIC.GetByID(structureReceived.ID);
				}
				// If the structure received is a Chat
				if (structureReceived is Chat)
				{
					// iterate through the Chats
					return Database.CHAT.GetByID(structureReceived.ID);
				}

				// Otherwise, if no match was found : return null
				return null;
			}


			/// <summary>
			/// Returns whether a Structure contains a Profile or not, thanks to its name
			/// </summary>
			/// <param name="structure">Structure in which we seek the Profile</param>
			/// <param name="profileName">Name of the Profile</param>
			/// <returns>Whether the Profile is contained in the Structure or not</returns>
			public static bool ContainsProfileName(Structure structure, string profileName)
			{
				// We iterate through the structure's Profiles
				foreach (Guid ID in structure.ID_members)
				{
					// If the names match
					if (profileName == PROFILE.GetByID(ID).name)
					{
						return true;
					}
				}

				// Otherwise, we return false
				return false;
			}


			/// <summary>
			/// Writes a Structure into an XML file
			/// </summary>
			/// <param name="structureReceived">Structure we want to write into an XML file</param>
			public static void Write(Structure structureReceived)
			{
				// If the structure received is a Topic
				if (structureReceived is Topic)
				{
					TOPIC.Write((Topic)structureReceived);
				}
				// If the structure received is a Chat
				if (structureReceived is Chat)
				{
					CHAT.Write((Chat)structureReceived);
				}
			}
		}


		public static class TOPIC
		{
			/// <summary>
			/// Reads all the XML files containing Topics
			/// </summary>
			public static void ReadAll()
			{
				DirectoryInfo d = new DirectoryInfo(pathTopics);
				FileInfo[] files = d.GetFiles("*.xml");

				// Iterating through all the xml Files
				foreach (FileInfo file in files)
				{
					// Reading the Topic
					Topic newTopic = Read(pathTopics + "/" + file.Name);

					// If not Null, add it to the Database
					if(newTopic != null)
					{
						topics.Add(newTopic);
					}
				}
			}


			/// <summary>
			/// Reads a given XML file containing a Topic, and adds it to the database
			/// </summary>
			/// <param name="file">Path of the file to read</param>
			private static Topic Read(string file)
			{
				// Creating a new default Topic
				Topic newTopic = new Topic();

				// Create a bool we'll use in composite elements
				bool continueReading;

				// Opening the XML file
				XmlReader xmlReader = XmlReader.Create(file);

				// Iterating throught the XML file
				while (xmlReader.Read())
				{
					// Filling the fields
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						switch (xmlReader.Name)
						{
							case "ID":
								newTopic.ID = Guid.Parse(xmlReader.ReadElementContentAsString());
								break;

							case "name":
								newTopic.name = xmlReader.ReadElementContentAsString();
								break;

							case "description":
								newTopic.description = xmlReader.ReadElementContentAsString();
								break;

							case "ID_members":
								// We only want to iterate through this very element, so we have to install some conditions
								continueReading = true;

								while (continueReading)
								{
									xmlReader.Read();
									if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "ID_member")
									{
										newTopic.ID_members.Add(Guid.Parse(xmlReader.ReadElementContentAsString()));
									}
									if (xmlReader.Name == "ID_members")
									{
										continueReading = false;
									}
								}
								break;

							case "ID_messages":
								// We only want to iterate through this very element, so we have to install some conditions
								continueReading = true;

								while (continueReading)
								{
									xmlReader.Read();
									if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "ID_message")
									{
										newTopic.ID_messages.Add(Guid.Parse(xmlReader.ReadElementContentAsString()));
									}
									if (xmlReader.Name == "ID_messages")
									{
										continueReading = false;
									}
								}
								break;

							case "ID_creator":
								newTopic.ID_creator = Guid.Parse(xmlReader.ReadElementContentAsString());
								break;

							case "dateCreation":
								newTopic.dateCreation = new DateTime(
									int.Parse(xmlReader.GetAttribute("year")),
									int.Parse(xmlReader.GetAttribute("month")),
									int.Parse(xmlReader.GetAttribute("day")),
									int.Parse(xmlReader.GetAttribute("hour")),
									int.Parse(xmlReader.GetAttribute("minute")),
									int.Parse(xmlReader.GetAttribute("second"))
									);
								break;
						}
					}
				}

				// We finish by closing the xmlReader
				xmlReader.Close();


				// Ensuring we have completed fields
				if (newTopic.ID != Guid.Empty && newTopic.name != "" && newTopic.description != "" && newTopic.ID_creator != Guid.Empty)
				{
					// Returning the new Topic
					return newTopic;
				}

				// Otherwise, we return null
				return null;
			}


			/// <summary>
			/// Writes a Topic structure into an XML file
			/// </summary>
			/// <param name="topic">Topic to transcribe into an XML file</param>
			public static void Write(Topic topic)
			{
				XmlTextWriter xmlWriter = new XmlTextWriter(pathTopics + "/" + topic.ID + ".xml", null);

				// Use automatic indentation for readability
				xmlWriter.Formatting = Formatting.Indented;

				// Write the root element
				xmlWriter.WriteStartElement("topic");

				// ID
				xmlWriter.WriteElementString("ID", topic.ID.ToString());

				// name
				xmlWriter.WriteElementString("name", topic.name);

				// description
				xmlWriter.WriteElementString("description", topic.description);

				// ID creator
				xmlWriter.WriteElementString("ID_creator", topic.ID_creator.ToString());

				// dateCreation
				xmlWriter.WriteStartElement("dateCreation");
				xmlWriter.WriteAttributeString("year", topic.dateCreation.Year.ToString());
				xmlWriter.WriteAttributeString("month", topic.dateCreation.Month.ToString());
				xmlWriter.WriteAttributeString("day", topic.dateCreation.Day.ToString());
				xmlWriter.WriteAttributeString("hour", topic.dateCreation.Hour.ToString());
				xmlWriter.WriteAttributeString("minute", topic.dateCreation.Minute.ToString());
				xmlWriter.WriteAttributeString("second", topic.dateCreation.Second.ToString());
				xmlWriter.WriteEndElement();

				// ID members
				xmlWriter.WriteStartElement("ID_members");
				foreach (Guid ID in topic.ID_members)
				{
					xmlWriter.WriteElementString("ID_member", ID.ToString());
				}
				xmlWriter.WriteFullEndElement();

				// ID messages
				xmlWriter.WriteStartElement("ID_messages");
				foreach (Guid ID in topic.ID_messages)
				{
					xmlWriter.WriteElementString("ID_message", ID.ToString());
				}
				xmlWriter.WriteFullEndElement();

				// end the root element
				xmlWriter.WriteFullEndElement();

				//Write the XML to file and close the writer
				xmlWriter.Close();
			}



			/// <summary>
			/// Returns a Topic of which we know the ID
			/// </summary>
			/// <param name="ID_given"> ID of the Topic we seek </param>
			/// <returns> The instance of the Topic if found, otherwise null </returns>
			public static Topic GetByID(Guid ID_given)
			{
				foreach (Topic topic in topics)
				{
					if (topic.ID == ID_given)
					{
						return topic;
					}
				}

				return null;
			}
		}


		public static class CHAT
		{
			/// <summary>
			/// Reads all the XML files containing Chats
			/// </summary>
			public static void ReadAll()
			{
				DirectoryInfo d = new DirectoryInfo(pathChats);
				FileInfo[] files = d.GetFiles("*.xml");

				// Iterating through all the xml Files
				foreach (FileInfo file in files)
				{
					// Reading the Chat
					Chat newChat = Read(pathChats + "/" + file.Name);

					// If not Null, add it to the Database
					if (newChat != null)
					{
						chats.Add(newChat);
					}
				}
			}


			/// <summary>
			/// Reads a given XML file containing a Chat, and adds it to the database
			/// </summary>
			/// <param name="file">Path of the file to read</param>
			private static Chat Read(string file)
			{
				// Creating a new default Chat
				Chat newChat = new Chat();

				// Create a bool we'll use in composite elements
				bool continueReading;

				// Opening the XML file
				XmlReader xmlReader = XmlReader.Create(file);

				// Iterating throught the XML file
				while (xmlReader.Read())
				{
					// Filling the fields
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						switch (xmlReader.Name)
						{
							case "ID":
								newChat.ID = Guid.Parse(xmlReader.ReadElementContentAsString());
								break;

							case "name":
								newChat.name = xmlReader.ReadElementContentAsString();
								break;

							case "description":
								newChat.description = xmlReader.ReadElementContentAsString();
								break;

							case "ID_members":
								// We only want to iterate through this very element, so we have to install some conditions
								continueReading = true;

								while (continueReading)
								{
									xmlReader.Read();
									if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "ID_member")
									{
										newChat.ID_members.Add(Guid.Parse(xmlReader.ReadElementContentAsString()));
									}
									if (xmlReader.Name == "ID_members")
									{
										continueReading = false;
									}
								}
								break;

							case "ID_messages":
								// We only want to iterate through this very element, so we have to install some conditions
								continueReading = true;

								while (continueReading)
								{
									xmlReader.Read();
									if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "ID_message")
									{
										newChat.ID_messages.Add(Guid.Parse(xmlReader.ReadElementContentAsString()));
									}
									if (xmlReader.Name == "ID_messages")
									{
										continueReading = false;
									}
								}
								break;


							case "dateCreation":
								newChat.dateCreation = new DateTime(
									int.Parse(xmlReader.GetAttribute("year")),
									int.Parse(xmlReader.GetAttribute("month")),
									int.Parse(xmlReader.GetAttribute("day")),
									int.Parse(xmlReader.GetAttribute("hour")),
									int.Parse(xmlReader.GetAttribute("minute")),
									int.Parse(xmlReader.GetAttribute("second"))
									);
								break;
						}
					}
				}

				// We finish by closing the xmlReader
				xmlReader.Close();


				// Ensuring we have completed fields
				if (newChat.ID != Guid.Empty && newChat.name != "" && newChat.description != "")
				{
					// Returning the new Chat
					return newChat;
				}

				// Otherwise, we return null
				return null;
			}


			/// <summary>
			/// Writes a Chat structure into an XML file
			/// </summary>
			/// <param name="chat">Chat to transcribe into an XML file</param>
			public static void Write(Chat chat)
			{
				XmlTextWriter xmlWriter = new XmlTextWriter(pathChats + "/" + chat.ID + ".xml", null);

				// Use automatic indentation for readability
				xmlWriter.Formatting = Formatting.Indented;

				// Write the root element
				xmlWriter.WriteStartElement("chat");

				// ID
				xmlWriter.WriteElementString("ID", chat.ID.ToString());

				// name
				xmlWriter.WriteElementString("name", chat.name);

				// description
				xmlWriter.WriteElementString("description", chat.description);

				// dateCreation
				xmlWriter.WriteStartElement("dateCreation");
				xmlWriter.WriteAttributeString("year", chat.dateCreation.Year.ToString());
				xmlWriter.WriteAttributeString("month", chat.dateCreation.Month.ToString());
				xmlWriter.WriteAttributeString("day", chat.dateCreation.Day.ToString());
				xmlWriter.WriteAttributeString("hour", chat.dateCreation.Hour.ToString());
				xmlWriter.WriteAttributeString("minute", chat.dateCreation.Minute.ToString());
				xmlWriter.WriteAttributeString("second", chat.dateCreation.Second.ToString());
				xmlWriter.WriteEndElement();

				// ID members
				xmlWriter.WriteStartElement("ID_members");
				foreach(Guid ID in chat.ID_members)
				{
					xmlWriter.WriteElementString("ID_member", ID.ToString());
				}
				xmlWriter.WriteFullEndElement();

				// ID messages
				xmlWriter.WriteStartElement("ID_messages");
				foreach (Guid ID in chat.ID_messages)
				{
					xmlWriter.WriteElementString("ID_message", ID.ToString());
				}
				xmlWriter.WriteFullEndElement();

				// end the root element
				xmlWriter.WriteFullEndElement();

				//Write the XML to file and close the writer
				xmlWriter.Close();
			}


			/// <summary>
			/// Deletes the XML file representing the structure
			/// </summary>
			/// <param name="chat">Chat of which we ant to delete the XML file</param>
			public static void Delete(Chat chat)
			{
				File.Delete(pathChats + "/" + chat.ID.ToString() + ".xml");
			}

			/// <summary>
			/// Returns a Chat of which we know the ID
			/// </summary>
			/// <param name="ID_given"> ID of the Chat we seek </param>
			/// <returns> The instance of the Chat if found, otherwise null </returns>
			public static Chat GetByID(Guid ID_given)
			{
				foreach (Chat chat in chats)
				{
					if (chat.ID == ID_given)
					{
						return chat;
					}
				}

				return null;
			}
		}


		public static class MESSAGE
		{
			/// <summary>
			/// Reads all the XML files containing Messages
			/// </summary>
			public static void ReadAll()
			{
				DirectoryInfo d = new DirectoryInfo(pathMessages);
				FileInfo[] files = d.GetFiles("*.xml");

				// Iterating through all the xml Files
				foreach (FileInfo file in files)
				{
					// Reading the Chat
					Message newMessage = Read(pathMessages + "/" + file.Name);

					// If not Null, add it to the Database
					if (newMessage != null)
					{
						messages.Add(newMessage);
					}
				}
			}


			/// <summary>
			/// Reads a given XML file containing a Message, and adds it to the database
			/// </summary>
			/// <param name="file">Path of the file to read</param>
			private static Message Read(string file)
			{
				// Creating a new default Message
				Message  newMessage = new Message();

				// Opening the XML file
				XmlReader xmlReader = XmlReader.Create(file);

				// Iterating throught the XML file
				do
				{
					// Filling the fields
					if (xmlReader.NodeType == XmlNodeType.Element)
					{
						switch (xmlReader.Name)
						{
							case "ID":
								newMessage.ID = Guid.Parse(xmlReader.ReadElementContentAsString());
								break;

							case "content":
								newMessage.content = xmlReader.ReadElementContentAsString();
								break;

							case "ID_author":
								newMessage.ID_author = Guid.Parse(xmlReader.ReadElementContentAsString());
								break;

							case "dateCreation":
								newMessage.dateCreation = new DateTime(
									int.Parse(xmlReader.GetAttribute("year")),
									int.Parse(xmlReader.GetAttribute("month")),
									int.Parse(xmlReader.GetAttribute("day")),
									int.Parse(xmlReader.GetAttribute("hour")),
									int.Parse(xmlReader.GetAttribute("minute")),
									int.Parse(xmlReader.GetAttribute("second"))
									);
								break;
						}
					}
				}
				while (xmlReader.Read());

				// We finish by closing the xmlReader
				xmlReader.Close();


				// Ensuring we have completed fields
				if (newMessage.ID != Guid.Empty && newMessage.content != "" && newMessage.ID_author != Guid.Empty)
				{
					// returns the Message
					return newMessage;
				}

				// Otherwise, we return null
				return null;
			}


			/// <summary>
			/// Writes a Message structure into an XML file
			/// </summary>
			/// <param name="message">Message to transcribe into an XML file</param>
			public static void Write(Message message)
			{
				XmlTextWriter xmlWriter = new XmlTextWriter(pathMessages + "/" + message.ID + ".xml", null);

				// Use automatic indentation for readability
				xmlWriter.Formatting = Formatting.Indented;

				// Write the root element
				xmlWriter.WriteStartElement("message");

				// ID
				xmlWriter.WriteElementString("ID", message.ID.ToString());

				// content
				xmlWriter.WriteElementString("content", message.content);

				// ID author
				xmlWriter.WriteElementString("ID_author", message.ID_author.ToString());

				// dateCreation
				xmlWriter.WriteStartElement("dateCreation");
				xmlWriter.WriteAttributeString("year", message.dateCreation.Year.ToString());
				xmlWriter.WriteAttributeString("month", message.dateCreation.Month.ToString());
				xmlWriter.WriteAttributeString("day", message.dateCreation.Day.ToString());
				xmlWriter.WriteAttributeString("hour", message.dateCreation.Hour.ToString());
				xmlWriter.WriteAttributeString("minute", message.dateCreation.Minute.ToString());
				xmlWriter.WriteAttributeString("second", message.dateCreation.Second.ToString());
				xmlWriter.WriteEndElement();


				// end the root element
				xmlWriter.WriteFullEndElement();

				//Write the XML to file and close the writer
				xmlWriter.Close();
			}
		}
	}
}