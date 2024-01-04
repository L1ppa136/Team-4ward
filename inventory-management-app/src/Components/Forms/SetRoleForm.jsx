import React from 'react';

const SetRoleForm = ({handleSubmit, formdata, handleInputChange}) => {
    return (
        <form className='setRoleForm' onSubmit={handleSubmit}>
                        <label>Username:</label>
                        <input type='text' name='userName' value={formdata.userName} onChange={handleInputChange} required />
                        <label>Role:</label>
                        <select name='role' defaultValue={formdata.role} onChange={handleInputChange} required>
                            <option value="" disabled>Select Role</option>
                            <option value={"Forklift Driver"}>Forklift Driver</option>
                            <option value={"Admin"}>Admin</option>
                            <option value={"Customer Planner"}>Customer Planner</option>
                            <option value={"Production Leader"}>Production Leader</option>
                            <option value={"Warehouse Leader"}>Warehouse Leader</option>
                        </select>
                        <button type='submit' className='submitBtn'>Submit</button>
                    </form>
    )
}
export default SetRoleForm;