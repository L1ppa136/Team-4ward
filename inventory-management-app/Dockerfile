# Use an official Node.js runtime as a base image
FROM node:18-alpine

# Set the working directory in the container
WORKDIR /inventory-management-app

# Copy package.json and package-lock.json to the container
COPY package*.json ./

# Install dependencies
RUN yarn install
RUN npm install
RUN npm install react-router-dom
RUN npm install axios

# Copy the remaining application files to the container
COPY . .

# Build the React app
RUN npm run build
RUN yarn build

# Expose the port that the app will run on
EXPOSE 3000

# Command to run the application
CMD ["npm", "start"]