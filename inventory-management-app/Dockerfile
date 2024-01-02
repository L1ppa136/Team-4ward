# Use an official Node.js runtime as a base image
FROM node:21-alpine

# Set the working directory in the container
WORKDIR /inventory-management-app

# Copy package.json and package-lock.json to the container
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the remaining application files to the container
COPY . .

# Command to run the application
CMD ["npm", "start"]