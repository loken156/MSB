import React, { useState, useEffect } from 'react';
import '../css/WarehousePage.css';
import { getAllWarehouses, addWarehouse } from '../API/warehouseService';
import { getAllShelves, addShelf } from '../API/shelfService';
import { getAddressById } from '../API/addressService';
import arrowImage from '../assets/arrow_input_fields.png';

function WarehousePage() {
  const [warehouses, setWarehouses] = useState([]);
  const [shelves, setShelves] = useState([]);
  const [addresses, setAddresses] = useState({});
  const [newWarehouse, setNewWarehouse] = useState({
    warehouseName: '',
    addressId: ''
  });
  const [newShelf, setNewShelf] = useState({
    shelfId: '',
    section: '',
    shelfRows: '',
    shelfColumn: '',
    largeBoxCapacity: '',
    mediumBoxCapacity: '',
    smallBoxCapacity: '',
    occupancy: false,
    warehouseName: '',
  });
  const [response, setResponse] = useState('');
  
  const [openSections, setOpenSections] = useState({});

  const fetchWarehousesAndShelves = async () => {
    try {
      const warehousesData = await getAllWarehouses();
      setWarehouses(warehousesData);

      const shelvesData = await getAllShelves();
      setShelves(shelvesData);

      const addressesData = {};
      for (const warehouse of warehousesData) {
        if (warehouse.addressId && warehouse.addressId !== "00000000-0000-0000-0000-000000000000") {
          try {
            const address = await getAddressById(warehouse.addressId);
            if (address) {
              addressesData[warehouse.addressId] = `${address.streetName}, ${address.unitNumber}, ${address.zipCode}`;
            } else {
              addressesData[warehouse.addressId] = "No address found";
            }
          } catch (error) {
            addressesData[warehouse.addressId] = "Address fetch error";
          }
        } else {
          addressesData[warehouse.addressId] = "No valid address ID";
        }
      }
      setAddresses(addressesData);
    } catch (error) {
      console.error('Error fetching warehouses, shelves, or addresses:', error.message);
    }
  };

  const handleCreateWarehouse = async (e) => {
    e.preventDefault();
    try {
      const createdWarehouse = await addWarehouse(newWarehouse);
      setResponse(`Warehouse created: ${createdWarehouse.warehouseName}`);
      setNewWarehouse({ warehouseName: '', addressId: '' });
      fetchWarehousesAndShelves();
    } catch (error) {
      console.error('Error creating warehouse:', error.message);
      setResponse(`Error: ${error.message}`);
    }
  };

  const handleCreateShelf = async (e) => {
    e.preventDefault();
    try {
      const createdShelf = await addShelf(newShelf);
      setResponse(`Shelf created: ${createdShelf.shelfId}`);
      setNewShelf({
        shelfId: '',
        section: '',
        shelfRows: '',
        shelfColumn: '',
        largeBoxCapacity: '',
        mediumBoxCapacity: '',
        smallBoxCapacity: '',
        occupancy: false,
        warehouseName: '',
      });
      fetchWarehousesAndShelves();
    } catch (error) {
      console.error('Error creating shelf:', error.message);
      setResponse(`Error creating shelf: ${error.message}`);
    }
  };

  const handleInputChange = (e, field) => {
    const value = e.target.value === '' ? '' : parseInt(e.target.value, 10);
    setNewShelf(prev => ({ ...prev, [field]: isNaN(value) ? '' : value }));
  };

  const getShelvesForWarehouse = (warehouseId) => {
    return shelves.filter(shelf => shelf.warehouseId === warehouseId);
  };

  const toggleSection = (warehouseId) => {
    setOpenSections(prev => ({
      ...prev,
      [warehouseId]: !prev[warehouseId],
    }));
  };

  useEffect(() => {
    fetchWarehousesAndShelves();
  }, []);

  return (
    <div className="warehouse_page">
      <h1 className="heading_warehouse margin_top_warehouse">Warehouse Information</h1>
      {warehouses.map(warehouse => (
        <div key={warehouse.warehouseId} className="warehouse_container margin_top_warehouse">
          <div className="section_header_warehouse" onClick={() => toggleSection(warehouse.warehouseId)}>
            <h3 className='h3_warehouse'>Warehouse Name: {warehouse.warehouseName}</h3>
            <img
              src={arrowImage}
              alt="Toggle arrow"
              className={`arrow_warehouse ${openSections[warehouse.warehouseId] ? 'open' : ''}`}
            />
          </div>
          <div className={`section_content_warehouse ${openSections[warehouse.warehouseId] ? 'open' : 'closed'}`}>
            <h4 className='h4_warehouse'>Address: {addresses[warehouse.addressId] || "No address available"}</h4>
            <h4 className='h4_warehouse'>Shelves in {warehouse.warehouseName}:</h4>
            <table className='table_warehouse'>
              <thead>
                <tr className='tr_warehouse'>
                  <th>Shelf ID</th>
                  <th>Section</th>
                  <th>Row</th>
                  <th>Column</th>
                  <th>Large Box Capacity</th>
                  <th>Medium Box Capacity</th>
                  <th>Small Box Capacity</th>
                  <th>Available Large Slots</th>
                  <th>Available Medium Slots</th>
                  <th>Available Small Slots</th>
                </tr>
              </thead>
              <tbody>
                {getShelvesForWarehouse(warehouse.warehouseId).map(shelf => (
                  <tr className='tr_warehouse' key={shelf.shelfId}>
                    <td>{shelf.shelfId}</td>
                    <td>{shelf.section}</td>
                    <td>{shelf.shelfRows}</td>
                    <td>{shelf.shelfColumn}</td>
                    <td>{shelf.largeBoxCapacity}</td>
                    <td>{shelf.mediumBoxCapacity}</td>
                    <td>{shelf.smallBoxCapacity}</td>
                    <td>{shelf.availableLargeSlots}</td>
                    <td>{shelf.availableMediumSlots}</td>
                    <td>{shelf.availableSmallSlots}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      ))}

      <div className="create_warehouse_form margin_top_warehouse">
        <h2 className='h2_warehouse'>Create New Warehouse</h2>
        <form onSubmit={handleCreateWarehouse}>
          <input
            className='input_warehouse'
            type="text"
            placeholder="Warehouse Name"
            value={newWarehouse.warehouseName}
            onChange={(e) => setNewWarehouse({ ...newWarehouse, warehouseName: e.target.value })}
            required
          />
          <input
            className='input_warehouse'
            type="text"
            placeholder="Address ID"
            value={newWarehouse.addressId}
            onChange={(e) => setNewWarehouse({ ...newWarehouse, addressId: e.target.value })}
            required
          />
          <button className='button_warehouse' type="submit">Create Warehouse</button>
        </form>
        {response && <p>{response}</p>}
      </div>

      <div className="create_shelf_form margin_top_warehouse">
        <h2 className='h2_warehouse'>Create New Shelf</h2>
        <form onSubmit={handleCreateShelf}>
          <input
            className='input_warehouse'
            type="text"
            placeholder="Shelf Section"
            value={newShelf.section}
            onChange={(e) => setNewShelf({ ...newShelf, section: e.target.value })}
            required
          />
          <input
            className='input_warehouse'
            type="number"
            placeholder="Shelf Rows"
            value={newShelf.shelfRows}
            onChange={(e) => handleInputChange(e, 'shelfRows')}
            min="1"
            required
          />
          <input
            className='input_warehouse'
            type="number"
            placeholder="Shelf Column"
            value={newShelf.shelfColumn}
            onChange={(e) => handleInputChange(e, 'shelfColumn')}
            min="1"
            required
          />
          <input
            className='input_warehouse'
            type="number"
            placeholder="Large Box Capacity"
            value={newShelf.largeBoxCapacity}
            onChange={(e) => handleInputChange(e, 'largeBoxCapacity')}
            required
          />
          <input
            className='input_warehouse'
            type="number"
            placeholder="Medium Box Capacity"
            value={newShelf.mediumBoxCapacity}
            onChange={(e) => handleInputChange(e, 'mediumBoxCapacity')}
            required
          />
          <input
            className='input_warehouse'
            type="number"
            placeholder="Small Box Capacity"
            value={newShelf.smallBoxCapacity}
            onChange={(e) => handleInputChange(e, 'smallBoxCapacity')}
            required
          />
          <input
            className='input_warehouse'
            type="text"
            placeholder="Warehouse Name"
            value={newShelf.warehouseName}
            onChange={(e) => setNewShelf({ ...newShelf, warehouseName: e.target.value })}
            required
          />
          <button className='button_warehouse' type="submit">Create Shelf</button>
        </form>
        {response && <p>{response}</p>}
      </div>
    </div>
  );
}

export default WarehousePage;
